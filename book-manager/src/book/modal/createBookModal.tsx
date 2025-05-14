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
import { createBook } from '../../services/bookService';
import React from 'react';

interface CreateBookModalProps {
  open: boolean;
  onClose: () => void;
  onSuccess: () => void;
}

export function CreateBookModal({ open, onClose, onSuccess }: CreateBookModalProps) {
  const [formData, setFormData] = useState({
    title: '',
    publisher: '',
    edition: '',
    publicationYear: ''
  });

  const [loading, setLoading] = useState(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async () => {
    if (!formData.title || !formData.publisher || !formData.edition || !formData.publicationYear) {
      alert("Todos os campos são obrigatórios.");
      return;
    }

    setLoading(true);

    const payload = {
      title: formData.title, 
      publisher: formData.publisher,
      edition: parseInt(formData.edition),
      publicationYear: formData.publicationYear
    };

    const result = await createBook(payload);
    setLoading(false);

    if (result) {
      alert("Livro criado com sucesso!");
      setFormData({
        title: '',
        publisher: '',
        edition: '',
        publicationYear: ''
      });
      onSuccess();
    } else {
      console.log("Erro ao criar livro.");
    }
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle sx={{ m: 0, p: 2 }}>
        Novo Livro
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
          label="Título"
          name="title"
          value={formData.title}
          onChange={handleChange}
          type="text"
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
          type="number"
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
          type="number"
          about=''
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
