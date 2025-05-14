import axios from "axios";

export interface BookAuthorEntity {
    authorId: number;
    nameAuthor: string;
    bookName: string;
    bookId: number;
}

export async function getBookAuthor(): Promise<BookAuthorEntity[]> {
    try {
        const response = await axios.get<BookAuthorEntity[]>('http://localhost:7224/BookAuthor');
        return response.data;
    } catch (error) {
        console.error('Erro ao buscar autores:', error);
        return [];
    }
}

export async function createBookAuthor(bookSubjectData: { bookId: number, authorId: number }): Promise<BookAuthorEntity | null> {
    try {
        const payload = {
            bookId: bookSubjectData.bookId,
            AuthorId: bookSubjectData.authorId
        };

        const response = await axios.post<BookAuthorEntity>(
            'http://localhost:7224/BookAuthor',
            payload,
            {
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        );

        return response.data;
    } catch (error) {
        console.error('Erro ao associar:', error);
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

export async function updateBookAuthor(
    payload: {
        originalBookId: number;
        originalAuhtorId: number;
        updatedBookId: number;
        updatedAuhtorId: number;
    }
): Promise<BookAuthorEntity | null> {
    try {
        const response = await axios.put<BookAuthorEntity>(
            `http://localhost:7224/BookAuthor`,
            {
                auhtorId: payload.originalAuhtorId,
                bookId: payload.originalBookId,
                BookNew: payload.updatedBookId
            },
            {
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        );

        return response.data;
    } catch (error) {
        console.error('Erro ao atualizar associar:', error);

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
export async function deleteBookAuthor(book: number, author: number): Promise<boolean> {
    try {
        const payload = {
            authorId: author,
            bookId: book
        };

        await axios.delete(`http://localhost:7224/BookAuthor`, { data: payload });
        return true;
    } catch (error) {
        console.error('Erro ao excluir associação livro-assunto:', error);
        return false;
    }
}
