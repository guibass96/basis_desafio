import axios from "axios";

export interface AssuntoEntity {
  id: number;
  description: string;
}

export async function getSubject(): Promise<AssuntoEntity[]> {
  try {
    const response = await axios.get<AssuntoEntity[]>('http://localhost:7224/Subject');
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar autores:', error);
    return [];
  }
}

export async function createSubject(Subject: Omit<AssuntoEntity, 'id'>): Promise<AssuntoEntity | null> {
  try {
    const response = await axios.post<AssuntoEntity>('http://localhost:7224/Subject', Subject);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar autor:', error);
    return null;
  }
}

export async function updateSubject(subject: AssuntoEntity): Promise<AssuntoEntity | null> {
  try {
    const response = await axios.put(`http://localhost:7224/Subject`, subject);
    return response.data;
  } catch (error) {
    console.error('Erro ao editar livro:', error);
    return null;
  }
}

export async function deleteSubject(SubjectId: number): Promise<boolean> {
  try {
    await axios.delete(`http://localhost:7224/Subject/${SubjectId}`);
    return true;
  } catch (error) {
    console.error('Erro ao excluir livro:', error);
    return false;
  }
}
