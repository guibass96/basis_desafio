import React, { useEffect, useState } from 'react';
import { BookEntity, getBooks } from '../services/bookService';
import { Box, Button, Container, Paper, Typography } from '@mui/material';
import PurchaseOptionTable from './PurchaseOptionTable';
import { getPurchaseOption, PurchaseOptionEntity } from '../services/PurchaseOption';
import { CreatePurchaseOptionModal } from './modal/createPurchaseOptionModal';

const PurchaseOption = () => {
    const [authors, setAuthors] = useState<PurchaseOptionEntity[]>([]);
    const [open, setOpen] = useState(false);

    const handleClickOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    async function fetchData() {
        const result = await getPurchaseOption();
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
                    Opções de Compra
                </Typography>
            </Paper>

            <Button variant="contained" onClick={handleClickOpen} sx={{ mb: 2 }}>
                Novo
            </Button>
            <CreatePurchaseOptionModal
                open={open}
                onClose={handleClose}
                onSuccess={() => {
                    setOpen(false);
                    fetchData();
                }}
            />


            <Box sx={{ height: '50vh', overflow: 'auto' }}>
                <PurchaseOptionTable
                    data={authors}
                    onSuccess={() => {
                        fetchData();
                    }}
                />
            </Box>
        </Container>

    );
}

export default PurchaseOption;