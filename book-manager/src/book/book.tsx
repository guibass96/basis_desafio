import React, { useEffect, useState } from 'react';
import { BookEntity, getBooks } from '../services/bookService';
import BookTable from './bookTable';
import { Box, Button, Container, Paper, Typography } from '@mui/material';
import { CreateBookModal } from './modal/createBookModal';
interface AuthorProps {
  name: string;
  bio: string;
  birthDate: string;
}

const Book = () => {
  const [authors, setAuthors] = useState<BookEntity[]>([]);
  const [open, setOpen] = useState(false);

  const handleClickOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  async function fetchData() {
    const result = await getBooks();
    setAuthors(result);
  }
  useEffect(() => {

    fetchData();
  }, []);


  return (
    <Container maxWidth="md">
      <Paper
        elevation={0}
        sx={{
          p: 2,
          mb: 2,
          backgroundColor: 'transparent',
          textAlign: 'center',
        }}
      >
        <Typography variant="h5" component="h2">
          Livros
        </Typography>
      </Paper>

      <Button variant="contained" onClick={handleClickOpen} sx={{ mb: 2 }}>
        Novo
      </Button>

      <CreateBookModal
        open={open}
        onClose={handleClose}
        onSuccess={() => {
          setOpen(false);
          fetchData();
        }}
      />

      <Box sx={{ height: '50vh', overflow: 'auto' }}>
        <BookTable
          data={authors}
          onSuccess={() => {
            fetchData();
          }}
        />
      </Box>
    </Container>

  );
}

export default Book;