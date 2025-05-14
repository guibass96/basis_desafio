import { useEffect, useState } from 'react';
import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    TextField,
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
import { createBookSubject } from '../../services/bookSubjectService'; // Supondo que existe este serviço
import React from 'react';

interface CreateBookSubjectModalProps {
    open: boolean;
    onClose: () => void;
    onSuccess: () => void;
}

export function CreateBookSubjectModal({ open, onClose, onSuccess }: CreateBookSubjectModalProps) {
    const [books, setBooks] = useState<BookEntity[]>([]);
    const [subjects, setSubjects] = useState<AssuntoEntity[]>([]);
    const [formData, setFormData] = useState({
        bookId: 0,
        subjectId: 0
    });
    const [loading, setLoading] = useState(false);

    const handleBookSelectChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setFormData({ ...formData, bookId: value });
    };

    const handleSubjectSelectChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setFormData({ ...formData, subjectId: value });
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

    const handleSubmit = async () => {
        if (!formData.bookId || !formData.subjectId) {
            alert("Todos os campos são obrigatórios.");
            return;
        }

        setLoading(true);

        try {
            const payload = {
                bookId: formData.bookId,
                subjectId: formData.subjectId
            };

            const result = await createBookSubject(payload);

            if (result) {
                setFormData({
                    bookId: 0,
                    subjectId: 0
                });
                onSuccess();
                onClose();
            } else {
                alert("Erro ao criar associação");
            }
        } catch (error) {
            console.error("Erro:", error);
            alert("Erro ao processar solicitação");
        } finally {
            setLoading(false);
        }
    };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle sx={{ m: 0, p: 2 }}>
                Associar Livro e Assunto
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
                        id="book-select"
                        name="bookId"
                        value={formData.bookId}
                        onChange={handleBookSelectChange}
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
                        id="subject-select"
                        name="subjectId"
                        value={formData.subjectId}
                        onChange={handleSubjectSelectChange}
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
                <Button onClick={handleSubmit} variant="contained" disabled={loading}>
                    {loading ? <CircularProgress size={24} /> : 'Salvar'}
                </Button>
            </DialogActions>
        </Dialog>
    );
}