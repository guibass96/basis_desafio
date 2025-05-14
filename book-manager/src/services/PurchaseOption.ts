import axios from "axios";

// src/services/authorService.ts
export interface PurchaseOptionEntity {
    id: number;
    price: number;
    bookId: number;
    nameBook: string;
    saleCategory: string;
}


export enum SaleCategory {
    BALCAO = 'Balcão',
    SelfService = 'Self-Service',
    Internet = 'Internet',
    EVENTO = 'Evento'
}

export async function getPurchaseOption(): Promise<PurchaseOptionEntity[]> {
    try {
        const response = await axios.get<PurchaseOptionEntity[]>('http://localhost:7224/BookPurchaseOption');
        return response.data;
    } catch (error) {
        console.error('Erro ao buscar opções:', error);
        return [];
    }
}

export async function createPurchaseOption(option: Omit<PurchaseOptionEntity, 'id'>): Promise<PurchaseOptionEntity | null> {
    try {
        const response = await axios.post<PurchaseOptionEntity>('http://localhost:7224/BookPurchaseOption', option);
        return response.data;
    } catch (error) {
        console.error('Erro ao criar autor:', error);
        return null;
    }
}

export async function updatePurchaseOption(option: PurchaseOptionEntity): Promise<PurchaseOptionEntity | null> {
    try {
        const requestData = {
            Id: option.id,
            BookId: option.bookId,
            Type: option.saleCategory,
            Price: option.price
        };

        const response = await axios.put(`http://localhost:7224/BookPurchaseOption`, requestData, {
            headers: {
                'Content-Type': 'application/json'
            }
        });

        return response.data;
    } catch (error) {
        console.error('Erro ao atualizar ', error);
        return null;
    }
}

export async function deletePurchaseOption(optionId: number): Promise<boolean> {
    try {
        await axios.delete(`http://localhost:7224/BookPurchaseOption/${optionId}`);
        return true;
    } catch (error) {
        console.error('Erro ao excluir:', error);
        return false;
    }
}
