import React, { useEffect, useState } from 'react';
import AuthorTable from './authorTable';
import { Box, Button, Container, Paper, Typography } from '@mui/material';
import { AuthorEntity, getAuthor } from '../services/authorService';
import { CreateAuthorModal } from './modal/createAuthorModal';


const Author = () => {
    const [authors, setAuthors] = useState<AuthorEntity[]>([]);
    const [open, setOpen] = useState(false);

    const handleClickOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    async function fetchData() {
        const result = await getAuthor();
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
                    Autores
                </Typography>
            </Paper>

            <Button variant="contained" onClick={handleClickOpen} sx={{ mb: 2 }}>
                Novo
            </Button>

            <CreateAuthorModal
                open={open}
                onClose={handleClose}
                onSuccess={() => {
                    setOpen(false);
                    fetchData();
                }}
            />

            <Box sx={{ height: '50vh', overflow: 'auto' }}>
                <AuthorTable
                    data={authors}
                    onSuccess={() => {
                        fetchData();
                    }}
                />
            </Box>
        </Container>
    );
}

export default Author;