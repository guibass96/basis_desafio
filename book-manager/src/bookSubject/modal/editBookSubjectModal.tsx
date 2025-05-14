import { useEffect, useState } from 'react';
import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    IconButton,
    CircularProgress,
    FormControl,
    InputLabel,
    MenuItem,
    Select,
    SelectChangeEvent
} from '@mui/material';
import { BookEntity, getBooks } from '../../services/bookService';
import { AssuntoEntity, getSubject } from '../../services/subjectService';
import { BookSubjectEntity, updateBookSubject } from '../../services/bookSubjectService';
import React from 'react';

interface EditBookSubjectModalProps {
    open: boolean;
    onClose: () => void;
    onSuccess: () => void;
    bookSubjectToEdit: BookSubjectEntity | null;
}

export function EditBookSubjectModal({ open, onClose, onSuccess, bookSubjectToEdit }: EditBookSubjectModalProps) {
    const [books, setBooks] = useState<BookEntity[]>([]);
    const [subjects, setSubjects] = useState<AssuntoEntity[]>([]);

    const [originalBookId, setOriginalBookId] = useState<number>(0);
    const [originalSubjectId, setOriginalSubjectId] = useState<number>(0);

    const [formData, setFormData] = useState({
        bookId: 0,
        subjectId: 0,
    });

    const [loading, setLoading] = useState(false);

    const handleBookSelectChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setFormData(prev => ({ ...prev, bookId: value }));
    };

    const handleSubjectSelectChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setFormData(prev => ({ ...prev, subjectId: value }));
    };

    async function fetchData() {
        try {
            const [booksResult, subjectsResult] = await Promise.all([
                getBooks(),
                getSubject()
            ]);
            setBooks(booksResult);
            setSubjects(subjectsResult);
        } catch (error) {
            console.error("Erro ao carregar dados:");
            alert("Erro ao carregar dados");
        }
    }

    useEffect(() => {
        fetchData();
    }, []);

  
    useEffect(() => {
        if (bookSubjectToEdit) {
            setOriginalBookId(bookSubjectToEdit.bookId);
            setOriginalSubjectId(bookSubjectToEdit.subjectId);

            setFormData({
                bookId: bookSubjectToEdit.bookId,
                subjectId: bookSubjectToEdit.subjectId,
            });
        } else {
            setOriginalBookId(0);
            setOriginalSubjectId(0);

            setFormData({
                bookId: 0,
                subjectId: 0,
            });
        }
    }, [bookSubjectToEdit]);

    const handleSubmit = async () => {
        if (!formData.bookId || !formData.subjectId) {
          alert("Todos os campos são obrigatórios.");
            return;
        }

        setLoading(true);

        try {
            const payload = {
                originalBookId,
                originalSubjectId,
                updatedBookId: formData.bookId,
                updatedSubjectId: formData.subjectId
            };

            const result = await updateBookSubject(payload);

            if (result) {
                onSuccess();
                onClose();
            } else {
                alert("Erro ao atualizar associação");
            }
        } catch (error) {
            console.error("Erro:", error);
            alert("Erro ao processar solicitação: ");
        } finally {
            setLoading(false);
        }
    };

    return (
        <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
            <DialogTitle sx={{ m: 0, p: 2 }}>
                Editar Livro e Assunto
                <IconButton
                    aria-label="close"
                    onClick={onClose}
                    sx={(theme) => ({
                        position: 'absolute',
                        right: 8,
                        top: 8,
                        color: theme.palette.grey[500],
                    })}
                />
            </DialogTitle>

            <DialogContent dividers>
                <FormControl fullWidth margin="dense" sx={{ mt: 2 }}>
                    <InputLabel id="book-select-label">Livro</InputLabel>
                    <Select
                        label="Livro"
                        labelId="book-select-label"
                        value={formData.bookId}
                        onChange={handleBookSelectChange}
                        disabled={loading}
                    >
                        <MenuItem value={0} disabled>
                            Selecione um livro
                        </MenuItem>
                        {books.map((book) => (
                            <MenuItem key={book.id} value={book.id}>
                                {book.title}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>

                <FormControl fullWidth margin="dense" sx={{ mt: 2 }}>
                    <InputLabel id="subject-select-label">Assunto</InputLabel>
                    <Select
                        label="Assunto"
                        labelId="subject-select-label"
                        value={formData.subjectId}
                        onChange={handleSubjectSelectChange}
                        disabled={loading}
                    >
                        <MenuItem value={0} disabled>
                            Selecione um assunto
                        </MenuItem>
                        {subjects.map((subject) => (
                            <MenuItem key={subject.id} value={subject.id}>
                                {subject.description}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>
            </DialogContent>

            <DialogActions>
                <Button onClick={onClose} disabled={loading}>Cancelar</Button>
                <Button
                    onClick={handleSubmit}
                    variant="contained"
                    disabled={loading}
                    color="primary"
                >
                    {loading ? <CircularProgress size={24} /> : 'Atualizar'}
                </Button>
            </DialogActions>
        </Dialog>
    );
}
