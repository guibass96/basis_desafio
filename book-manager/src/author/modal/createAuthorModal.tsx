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
import { createAuthor } from '../../services/authorService';

interface CreateModalProps {
  open: boolean;
  onClose: () => void;
  onSuccess: () => void;
}

export function CreateAuthorModal({ open, onClose, onSuccess }: CreateModalProps) {
  const [formData, setFormData] = useState({
    name: '',
  });

  const [loading, setLoading] = useState(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async () => {
    if (!formData.name) {
      alert("Todos os campos são obrigatórios.");
      return;
    }

    setLoading(true);

    const payload = {
      name: formData.name,

    };

    const result = await createAuthor(payload);
    setLoading(false);

    if (result) {
      setFormData({
        name: ''
      });
      onSuccess();
    } else {
      alert("Erro");
    }
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle sx={{ m: 0, p: 2 }}>
        Novo Autor
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
          name="name"
          value={formData.name}
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
