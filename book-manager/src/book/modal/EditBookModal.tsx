import React, { useState, useEffect } from 'react';
import { Dialog, DialogTitle, DialogContent, DialogActions, Button, TextField, CircularProgress } from '@mui/material';
import { BookEntity } from '../../services/bookService';
import { updateBook } from '../../services/bookService';

interface EditBookModalProps {
  open: boolean;
  onClose: () => void;
  bookToEdit: BookEntity | null;
  onSuccess: () => void;
}

const EditBookModal = ({ open, onClose, bookToEdit, onSuccess }: EditBookModalProps) => {
  const [formData, setFormData] = useState<BookEntity | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setFormData(bookToEdit || null);
  }, [bookToEdit]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    console.log(e)
    if (formData) {
      setFormData({ ...formData, [e.target.name]: e.target.value });
    }
  };

  const handleSubmit = async () => {
    if (!formData) return;
    setLoading(true);
    const result = await updateBook(formData);
    setLoading(false);
    
    if (result) {
      onSuccess();
      onClose();
    } else {
      console.log("Erro ao editar livro.");
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
              name="title"
              value={formData.title}
              onChange={handleChange}
              fullWidth
              margin="dense"
            />
            <TextField
              label="Editora"
              name="publisher"
              value={formData.publisher}
              onChange={handleChange}
              fullWidth
              margin="dense"
            />
            <TextField
              label="Edição"
              name="edition"
              value={formData.edition}
              onChange={handleChange}
              fullWidth
              margin="dense"
            />
            <TextField
              label="Ano de Publicação"
              name="publicationYear"
              value={formData.publicationYear}
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

export default EditBookModal;
