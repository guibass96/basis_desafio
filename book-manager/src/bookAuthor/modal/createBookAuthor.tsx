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
import React from 'react';
import { AuthorEntity, getAuthor } from '../../services/authorService';
import { createBookAuthor, getBookAuthor } from '../../services/bookAuthor';

interface CreateBookSubjectModalProps {
    open: boolean;
    onClose: () => void;
    onSuccess: () => void;
}

export function CreateBookAuthorModal({ open, onClose, onSuccess }: CreateBookSubjectModalProps) {
    const [books, setBooks] = useState<BookEntity[]>([]);
    const [authors, setAuthors] = useState<AuthorEntity[]>([]);
    const [formData, setFormData] = useState({
        bookId: 0,
        authorId: 0
    });
    const [loading, setLoading] = useState(false);

    const handleBookSelectChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setFormData({ ...formData, bookId: value });
    };

    const handleAuthorSelectChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setFormData({ ...formData, authorId: value });
    };

    async function fetchData() {
        try {
            const [booksResult, subjectsResult] = await Promise.all([
                getBooks(),
                getAuthor()
            ]);
            setBooks(booksResult);
            setAuthors(subjectsResult);
        } catch (error) {
            alert("Erro ao carregar dados");
        }
    }

    useEffect(() => {
        fetchData();
    }, []);

    const handleSubmit = async () => {
        if (!formData.bookId || !formData.authorId) {
            alert("Todos os campos são obrigatórios.");
            return;
        }

        setLoading(true);

        try {
            const payload = {
                bookId: formData.bookId,
                authorId: formData.authorId
            };

            const result = await createBookAuthor(payload);

            if (result) {
                setFormData({
                    bookId: 0,
                    authorId: 0
                });
                onSuccess();
                onClose();
            } else {
                alert("Erro ao criar ");
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
                    <InputLabel id="autor-select-label">Autor</InputLabel>
                    <Select
                        label="Autor"
                        labelId="autor-select-label"
                        id="autor-select"
                        name="autorId"
                        value={formData.authorId}
                        onChange={handleAuthorSelectChange}
                    >
                        <MenuItem value={0} disabled>
                            Selecione um assunto
                        </MenuItem>
                        {authors.map((subject) => (
                            <MenuItem key={subject.id} value={subject.id}>
                                {subject.name}
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