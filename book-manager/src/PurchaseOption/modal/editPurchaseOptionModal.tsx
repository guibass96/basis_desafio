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
import { createPurchaseOption, PurchaseOptionEntity, SaleCategory, updatePurchaseOption } from '../../services/PurchaseOption';
import React from 'react';

interface CreatePurchaseOptionModalProps {
  open: boolean;
  onClose: () => void;
  onSuccess: () => void;
  purchaseToEdit: PurchaseOptionEntity | null;
}

// Função para formatar valor para moeda
const formatCurrency = (value: number | undefined): string => {
  if (!value) return '';
  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  }).format(value);
};

// Função para converter valor formatado de volta para número
const parseCurrency = (value: string): number => {
  const cleanValue = value.replace(/\D/g, '');
  return parseFloat(cleanValue) / 100;
};

export function EditCreatePurchaseOptionModal({ open, onClose, purchaseToEdit, onSuccess }: CreatePurchaseOptionModalProps) {
  const [books, setBooks] = useState<BookEntity[]>([]);
  const [loading, setLoading] = useState(false);
  const [priceInput, setPriceInput] = useState(''); // Estado para o valor formatado
  
  const [formData, setFormData] = useState<{
    id?: number;
    price?: number;
    bookId?: number;
    saleCategory?: SaleCategory;
  }>({
    id: 0,
    price: undefined,
    bookId: undefined,
    saleCategory: undefined
  });

  // Atualiza o formData quando purchaseToEdit muda
  useEffect(() => {
    if (purchaseToEdit) {
      setFormData({
        id: purchaseToEdit.id,
        price: purchaseToEdit.price,
        bookId: purchaseToEdit.bookId,
        saleCategory: purchaseToEdit.saleCategory  as SaleCategory
      });
      setPriceInput(formatCurrency(purchaseToEdit.price));
    } else {
      setFormData({
        id: 0,
        price: undefined,
        bookId: undefined,
        saleCategory: undefined
      });
      setPriceInput('');
    }
  }, [purchaseToEdit]);

  const handlePriceChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const rawValue = e.target.value;
    setPriceInput(rawValue);
    
    // Atualiza o formData com o valor numérico
    const numericValue = parseCurrency(rawValue);
    setFormData({ ...formData, price: numericValue });
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
    if (!formData.price || !formData.bookId || !formData.saleCategory) {
      alert("Todos os campos são obrigatórios.");
      return;
    }

    setLoading(true);

    const payload = {
      price: formData.price, 
      bookId: formData.bookId,
      saleCategory: formData.saleCategory,
      id: formData.id??0,
      nameBook: ""
    };

    try {
      const result = await updatePurchaseOption(payload);
      if (result) {
        onSuccess();
        onClose();
      } else {
        alert("Erro ao salvar");
      }
    } catch (error) {
      alert("Erro: " + (error as Error).message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle sx={{ m: 0, p: 2 }}>
        {purchaseToEdit ? 'Editar' : 'Nova'} Opção de Compra
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
            value={formData.bookId || ''}
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
            value={formData.saleCategory || ''}
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