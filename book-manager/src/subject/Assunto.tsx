import React, { useEffect, useState } from 'react';
import { Box, Button, Container, Paper, Typography } from '@mui/material';
import AssuntoTable from './AssuntoTable';
import { AssuntoEntity, getSubject } from '../services/subjectService';
import { CreateSubjectModal } from './modal/createSubjectModal';


const Assunto = () => {
    const [authors, setAuthors] = useState<AssuntoEntity[]>([]);
    const [open, setOpen] = useState(false);

    const handleClickOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    async function fetchData() {
        const result = await getSubject();
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
                    Assunto
                </Typography>
            </Paper>

            <Button variant="contained" onClick={handleClickOpen} sx={{ mb: 2 }}>
                Novo
            </Button>

            <CreateSubjectModal
                open={open}
                onClose={handleClose}
                onSuccess={() => {
                    setOpen(false);
                    fetchData();
                }}
            />

            <Box sx={{ height: '50vh', overflow: 'auto' }}>
                <AssuntoTable
                    data={authors}
                    onSuccess={() => {
                        fetchData();
                    }}
                />
            </Box>
        </Container>

    );
}

export default Assunto;