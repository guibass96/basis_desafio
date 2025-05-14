import axios from "axios";

export interface BookSubjectEntity {
  bookName: string;
  bookId: number;
  subjectId: number;
  subjectIdOld: number;
  SubjectNew: number;
  subjectionDescription: string;
}

export async function getBookSubject(): Promise<BookSubjectEntity[]> {
  try {
    const response = await axios.get<BookSubjectEntity[]>('http://localhost:7224/BookSubject');
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar autores:', error);
    return [];
  }
}

export async function createBookSubject(bookSubjectData: { bookId: number, subjectId: number }): Promise<BookSubjectEntity | null> {
  try {
    // Criando o payload apenas com os campos necessários
    const payload = {
      bookId: bookSubjectData.bookId,
      subjectId: bookSubjectData.subjectId
    };

    const response = await axios.post<BookSubjectEntity>(
      'http://localhost:7224/BookSubject',
      payload,
      {
        headers: {
          'Content-Type': 'application/json'
        }
      }
    );

    return response.data;
  } catch (error) {
    console.error('Erro ao associar livro e assunto:', error);

    // Melhor tratamento de erro
    if (axios.isAxiosError(error)) {
      console.error('Detalhes do erro:', {
        status: error.response?.status,
        data: error.response?.data,
        message: error.message
      });
    }

    return null;
  }
}

export async function updateBookSubject(
  payload: {
    originalBookId: number;
    originalSubjectId: number;
    updatedBookId: number;
    updatedSubjectId: number;
  }
): Promise<BookSubjectEntity | null> {
  try {


    const response = await axios.put<BookSubjectEntity>(
      `http://localhost:7224/BookSubject`,
      {
        subjectId: payload.originalSubjectId,
        bookId: payload.originalBookId,
        subjectNew: payload.updatedSubjectId
      },
      {
        headers: {
          'Content-Type': 'application/json'
        }
      }
    );

    return response.data;
  } catch (error) {
    console.error('Erro ao atualizar associação livro-assunto:', error);

    if (axios.isAxiosError(error)) {
      console.error('Detalhes do erro:', {
        status: error.response?.status,
        data: error.response?.data,
        message: error.message
      });
      throw new Error(error.response?.data?.message || 'Erro ao atualizar associação');
    }

    throw new Error('Erro desconhecido ao atualizar associação');
  }
}
export async function deleteBookSubject(book: number, subject: number): Promise<boolean> {
  try {
    const payload = {
      subjectId: subject,
      bookId: book
    };

    await axios.delete(`http://localhost:7224/BookSubject`, { data: payload });
    return true;
  } catch (error) {
    console.error('Erro ao excluir associação livro-assunto:', error);
    return false;
  }
}
