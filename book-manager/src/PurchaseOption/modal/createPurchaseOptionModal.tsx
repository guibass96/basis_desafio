import { useEffect, useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  IconButton,
  CircularProgress,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent
} from '@mui/material';
import { BookEntity, getBooks } from '../../services/bookService';
import React from 'react';
import { createPurchaseOption, SaleCategory } from '../../services/PurchaseOption';



interface CreatePurchaseOptionModalProps {
  open: boolean;
  onClose: () => void;
  onSuccess: () => void;
}

export function CreatePurchaseOptionModal({ open, onClose, onSuccess }: CreatePurchaseOptionModalProps) {
  const [books, setBooks] = useState<BookEntity[]>([]);
  const [priceInput, setPriceInput] = useState(''); 
  const [formData, setFormData] = useState({
    price: 0,
    bookId: 0,
    saleCategory: '' as SaleCategory
  });
  const [loading, setLoading] = useState(false);


  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };


  const handleBookSelectChange = (event: SelectChangeEvent<number>) => {
    const value = event.target.value as number;
    setFormData({ ...formData, bookId: value });
  };

  const handleCategorySelectChange = (event: SelectChangeEvent<SaleCategory>) => {
    const value = event.target.value as SaleCategory;
    setFormData({ ...formData, saleCategory: value });
  };

  async function fetchData() {
    const result = await getBooks();
    setBooks(result);
  }

  useEffect(() => {
    fetchData();
  }, []);

  const handleSubmit = async () => {
    if (!formData.price || formData.bookId === 0 || !formData.saleCategory) {
      alert("Todos os campos são obrigatórios.");
      return;
    }

    setLoading(true);

    const payload = {
      price: formData.price,
      bookId: formData.bookId,
      saleCategory: formData.saleCategory,
      nameBook: "",
      id: 0
    };

 
    const result = await createPurchaseOption(payload);
    setLoading(false);

    if (result) {
      setFormData({
        price: 0,
        bookId: 0,
        saleCategory: '' as SaleCategory
      });
      onSuccess();
    } else {
      alert("Erro");
    }
  };
  const parseCurrency = (value: string): number => {
  const cleanValue = value.replace(/\D/g, '');
  return parseFloat(cleanValue) / 100;
};
 const handlePriceChange = (e: React.ChangeEvent<HTMLInputElement>) => {
      const rawValue = e.target.value;
      setPriceInput(rawValue);
      
      const numericValue = parseCurrency(rawValue);
      setFormData({ ...formData, price: numericValue });
    };
  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle sx={{ m: 0, p: 2 }}>
        Nova Opção de Compra
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
             label="Preço"
             name="price"
             value={priceInput}
             onChange={handlePriceChange}
             fullWidth
             margin="dense"
             inputProps={{
               inputMode: 'numeric',
             }}
           />

        <FormControl fullWidth margin="dense" sx={{ mt: 2 }}>
          <InputLabel id="book-select-label">Livro</InputLabel>
          <Select
            label="Livro"
            labelId="book-select-label"
            id="book-select"
            name="bookId"
            value={formData.bookId}
            onChange={handleBookSelectChange}
          >
            {books.map((book) => (
              <MenuItem key={book.id} value={book.id}>
                {book.title}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <FormControl fullWidth margin="dense" sx={{ mt: 2 }}>
          <InputLabel id="category-select-label">Categoria de Venda</InputLabel>
          <Select
            label="Categoria de Venda"
            labelId="category-select-label"
            id="category-select"
            name="saleCategory"
            value={formData.saleCategory}
            onChange={handleCategorySelectChange}
          >
            {Object.values(SaleCategory).map((category) => (
              <MenuItem key={category} value={category}>
                {category}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
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