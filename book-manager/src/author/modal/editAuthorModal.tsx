import React, { useState, useEffect } from 'react';
import { Dialog, DialogTitle, DialogContent, DialogActions, Button, TextField, CircularProgress } from '@mui/material';
import { AuthorEntity, updateAuthor } from '../../services/authorService';

interface EditAuthorModalProps {
  open: boolean;
  onClose: () => void;
  authorToEdit: AuthorEntity | null;
  onSuccess: () => void;
}

const EditAuthorModal = ({ open, onClose, authorToEdit, onSuccess }: EditAuthorModalProps) => {
  const [formData, setFormData] = useState<AuthorEntity | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setFormData(authorToEdit || null);
  }, [authorToEdit]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (formData) {
      setFormData({ ...formData, [e.target.name]: e.target.value });
    }
  };

  const handleSubmit = async () => {
    if (!formData?.name) {
      alert("Todos os campos são obrigatórios.");
      return

    };
    setLoading(true);
    const result = await updateAuthor(formData);
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
      <DialogTitle>Editar Autor</DialogTitle>
      <DialogContent>
        {formData ? (
          <>
            <TextField
              label="Nome Autor"
              name="name"
              value={formData.name}
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

export default EditAuthorModal;
