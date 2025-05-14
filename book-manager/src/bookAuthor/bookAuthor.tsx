import React, { useEffect, useState } from 'react';
import { Box, Button, Container, Paper, Typography } from '@mui/material';
import BookAuthorTable from './bookAuthorTable';
import { BookAuthorEntity, getBookAuthor } from '../services/bookAuthor';
import { CreateBookAuthorModal } from './modal/createBookAuthor';

const BookAuthor = () => {
  const [authors, setAuthors] = useState<BookAuthorEntity[]>([]);
  const [open, setOpen] = useState(false);

  const handleClickOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  async function fetchData() {
    const result = await getBookAuthor();
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
          Livro por Autor
        </Typography>
      </Paper>

      <Button variant="contained" onClick={handleClickOpen} sx={{ mb: 2 }}>
        Novo
      </Button>
      <CreateBookAuthorModal
        open={open}
        onClose={handleClose}
        onSuccess={() => {
          setOpen(false);
          fetchData();
        }}
      />
      <Box sx={{ height: '50vh', overflow: 'auto' }}>
        <BookAuthorTable
          data={authors}
          onSuccess={() => {
            fetchData();
          }}
        />
      </Box>
    </Container>

  );
}

export default BookAuthor;