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
import React from 'react';
import { BookAuthorEntity, updateBookAuthor } from '../../services/bookAuthor';
import { AuthorEntity, getAuthor } from '../../services/authorService';

interface EditBookSubjectModalProps {
    open: boolean;
    onClose: () => void;
    onSuccess: () => void;
    bookAuthorToEdit: BookAuthorEntity | null;
}

export function EditBookAuthorModal({ open, onClose, onSuccess, bookAuthorToEdit }: EditBookSubjectModalProps) {
    const [books, setBooks] = useState<BookEntity[]>([]);
    const [subjects, setSubjects] = useState<AuthorEntity[]>([]);

    const [originalBookId, setOriginalBookId] = useState<number>(0);
    const [originalAuhtorId, setoriginalAuhtorId] = useState<number>(0);

    const [formData, setFormData] = useState({
        bookId: 0,
        authorId: 0,
    });

    const [loading, setLoading] = useState(false);

    const handleBookSelectChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setFormData(prev => ({ ...prev, bookId: value }));
    };

    const handleAuthorSelectChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setFormData(prev => ({ ...prev, authorId: value }));
    };

    async function fetchData() {
        try {
            const [booksResult, subjectsResult] = await Promise.all([
                getBooks(),
                getAuthor()
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
        if (bookAuthorToEdit) {
            setOriginalBookId(bookAuthorToEdit.bookId);
            setoriginalAuhtorId(bookAuthorToEdit.authorId);

            setFormData({
                bookId: bookAuthorToEdit.bookId,
                authorId: bookAuthorToEdit.authorId,
            });
        } else {
            setOriginalBookId(0);
            setoriginalAuhtorId(0);

            setFormData({
                bookId: 0,
                authorId: 0,
            });
        }
    }, [bookAuthorToEdit]);

    const handleSubmit = async () => {
        if (!formData.bookId || !formData.authorId) {
            alert("Todos os campos são obrigatórios.");
            return;
        }

        setLoading(true);

        try {
            const payload = {
                originalBookId,
                originalAuhtorId,
                updatedBookId: formData.bookId,
                updatedAuhtorId: formData.authorId
            };

            const result = await updateBookAuthor(payload);

            if (result) {
                onSuccess();
                onClose();
            } else {
                alert("Erro ao atualizar");
            }
        } catch (error) {
            console.error("Erro:", error);
            alert("Erro ao processar solicitação");
        } finally {
            setLoading(false);
        }
    };

    return (
        <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
            <DialogTitle sx={{ m: 0, p: 2 }}>
                Editar Associação Livro-Assunto
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
                    <InputLabel id="subject-select-label">Autor</InputLabel>
                    <Select
                        label="Assunto"
                        labelId="authorId-select-label"
                        value={formData.authorId}
                        onChange={handleAuthorSelectChange}
                        disabled={true}
                    >
                        <MenuItem value={0} disabled>
                            Selecione um assunto
                        </MenuItem>
                        {subjects.map((subject) => (
                            <MenuItem key={subject.id} value={subject.id}>
                                {subject.name}
                            </MenuItem>
                        ))}

                    </Select>
                </FormControl>


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
