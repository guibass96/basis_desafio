import axios from "axios";

// src/services/authorService.ts
export interface BookEntity {
  id: number;
  title: string;
  publisher: string;
  edition: number
  publicationYear: string
}
export interface BookPurchaseOption {
  purchaseType: string;
  price: number;
}
export async function getBooks(): Promise<BookEntity[]> {
  try {
    const response = await axios.get<BookEntity[]>('http://localhost:7224/Book');
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar autores:', error);
    return [];
  }
}

export async function createBook(author: Omit<BookEntity, 'id'>): Promise<BookEntity | null> {
  try {
    const response = await axios.post<BookEntity>('http://localhost:7224/Book', author);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar autor:', error);
    return null;
  }
}

export async function updateBook(book: BookEntity): Promise<BookEntity | null> {
  try {
    const response = await axios.put(`http://localhost:7224/Book`, book);
    return response.data;
  } catch (error) {
    console.error('Erro ao editar livro:', error);
    return null;
  }
}

export async function deleteBook(bookId: number): Promise<boolean> {
  try {
    await axios.delete(`http://localhost:7224/Book/${bookId}`);
    return true;
  } catch (error) {
    console.error('Erro ao excluir livro:', error);
    return false;
  }
}
