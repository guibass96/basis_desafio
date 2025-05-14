import { useState } from 'react';
import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    TextField,
    IconButton,
    CircularProgress
} from '@mui/material';
import React from 'react';
import { createSubject } from '../../services/subjectService';

interface CreateBookModalProps {
    open: boolean;
    onClose: () => void;
    onSuccess: () => void;
}

export function CreateSubjectModal({ open, onClose, onSuccess }: CreateBookModalProps) {
    const [formData, setFormData] = useState({
        description: '',
    });

    const [loading, setLoading] = useState(false);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async () => {
        if (!formData.description) {
            alert("Todos os campos são obrigatórios.");
            return;
        }

        setLoading(true);

        const payload = {
            description: formData.description,

        };

        const result = await createSubject(payload);
        setLoading(false);

        if (result) {
            setFormData({
                description: ''
            });
            onSuccess();
        } else {
            alert("Erro");
        }
    };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle sx={{ m: 0, p: 2 }}>
                Novo autor
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
                <TextField
                    label="nome"
                    name="description"
                    value={formData.description}
                    onChange={handleChange}
                    type="text"
                    fullWidth
                    margin="dense"
                />

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
