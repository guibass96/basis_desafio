import React, { useEffect, useState } from 'react';
import { Box, Button, Container, Paper, Typography } from '@mui/material';
import { BookSubjectEntity, getBookSubject } from '../services/bookSubjectService';
import BookSubjectTable from './BookSubjectTable';
import { CreateBookSubjectModal } from './modal/createBookSubjectModal';
interface AuthorProps {
  name: string;
  bio: string;
  birthDate: string;
}

const BookSubjects = () => {
  const [authors, setAuthors] = useState<BookSubjectEntity[]>([]);
  const [open, setOpen] = useState(false);

  const handleClickOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  async function fetchData() {
    const result = await getBookSubject();
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
      <CreateBookSubjectModal
        open={open}
        onClose={handleClose}
        onSuccess={() => {
          setOpen(false);
          fetchData();
        }}
      />
      <Box sx={{ height: '50vh', overflow: 'auto' }}>
        <BookSubjectTable
          data={authors}
          onSuccess={() => {
            fetchData();
          }}
        />
      </Box>
    </Container>

  );
}

export default BookSubjects;