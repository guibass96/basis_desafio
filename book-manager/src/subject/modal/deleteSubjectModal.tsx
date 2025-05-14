import React from 'react';
import { Dialog, DialogTitle, DialogActions, Button, CircularProgress } from '@mui/material';
import { deleteAuthor } from '../../services/authorService';

interface DeleteBookModalProps {
  open: boolean;
  onClose: () => void;
  bookId: number | null;
  onSuccess: () => void;
}

const DeleteSubjectModal = ({ open, onClose, bookId, onSuccess }: DeleteBookModalProps) => {
  const [loading, setLoading] = React.useState(false);

  const handleDelete = async () => {
    if (bookId === null) return;
    setLoading(true);
    const result = await deleteAuthor(bookId);
    setLoading(false);
    
    if (result) {
      onSuccess();
      onClose();
    } else {
      alert("Erro ao excluir livro.");
    }
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Excluir Livro</DialogTitle>
      <DialogActions>
        <Button onClick={onClose} disabled={loading}>Cancelar</Button>
        <Button onClick={handleDelete} variant="contained" disabled={loading}>
          {loading ? <CircularProgress size={24} /> : 'Excluir'}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default DeleteSubjectModal;
