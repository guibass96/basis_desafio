import React, { useState, useEffect } from 'react';
import { Dialog, DialogTitle, DialogContent, DialogActions, Button, TextField, CircularProgress } from '@mui/material';
import { AuthorEntity, updateAuthor } from '../../services/authorService';
import { AssuntoEntity, updateSubject } from '../../services/subjectService';

interface EditAuthorModalProps {
    open: boolean;
    onClose: () => void;
    subjectToEdit: AssuntoEntity | null;
    onSuccess: () => void;
}

const EditSubjectModal = ({ open, onClose, subjectToEdit, onSuccess }: EditAuthorModalProps) => {
    const [formData, setFormData] = useState<AssuntoEntity | null>(null);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        setFormData(subjectToEdit || null);
    }, [subjectToEdit]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (formData) {
            setFormData({ ...formData, [e.target.name]: e.target.value });
        }
    };

    const handleSubmit = async () => {
        if (!formData) return;
        setLoading(true);
        const result = await updateSubject(formData);
        setLoading(false);

        if (result) {
            onSuccess();
            onClose();
        } else {
            alert("Erro");
        }
    };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>Editar Livro</DialogTitle>
            <DialogContent>
                {formData ? (
                    <>
                        <TextField
                            label="Título"
                            name="description"
                            value={formData.description}
                            onChange={handleChange}
                            fullWidth
                            margin="dense"
                        />

                    </>
                ) : (
                    <CircularProgress />
                )}
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose} disabled={loading}>Cancelar</Button>
                <Button onClick={handleSubmit} variant="contained" disabled={loading}>
                    {loading ? <CircularProgress size={24} /> : 'Salvar'}
                </Button>
            </DialogActions>
        </Dialog>
    );
};

export default EditSubjectModal;
