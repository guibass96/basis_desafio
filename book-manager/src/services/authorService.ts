import axios from "axios";

export interface AuthorEntity {
  id: number;
  name: string;
}

export async function getAuthor(): Promise<AuthorEntity[]> {
  try {
    const response = await axios.get<AuthorEntity[]>('http://localhost:7224/api/Author');
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar:', error);
    return [];
  }
}

export async function createAuthor(author: Omit<AuthorEntity, 'id'>): Promise<AuthorEntity | null> {
  try {
    const response = await axios.post<AuthorEntity>('http://localhost:7224/api/Author', author);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar:', error);
    return null;
  }
}

export async function updateAuthor(book: AuthorEntity): Promise<AuthorEntity | null> {
  try {
    const response = await axios.put(`http://localhost:7224/api/Author`, book);
    return response.data;
  } catch (error) {
    console.error('Erro ao editar:', error);
    return null;
  }
}

export async function deleteAuthor(authorId: number): Promise<boolean> {
  try {
    await axios.delete(`http://localhost:7224/api/Author/${authorId}`);
    return true;
  } catch (error) {
    console.error('Erro ao excluir:', error);
    return false;
  }
}
