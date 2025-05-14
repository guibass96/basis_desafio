import * as React from 'react';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from '@mui/material/IconButton';
import { BookAuthorEntity, deleteBookAuthor } from '../services/bookAuthor';
import { EditBookAuthorModal } from './modal/editBookAuthor';

type ColumnId = keyof BookAuthorEntity | 'actions';

interface Column {
    id: ColumnId;
    label: string;
    minWidth?: number;
    align?: 'right' | 'left' | 'center';
    hidden?: boolean;
}

const columns: Column[] = [
    { id: 'bookName', label: 'Nome Livro', minWidth: 150 },
    { id: 'nameAuthor', label: 'Nome Autor', minWidth: 150 },
    { id: 'actions', label: 'Ações', minWidth: 150 },
];

interface ColumnGroupingTableProps {
    data: BookAuthorEntity[];
    onSuccess: () => void;
}

export default function ColumnGroupingTable({ data, onSuccess }: ColumnGroupingTableProps) {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(10);
    const [open, setOpen] = React.useState(false);
    const [bookAuthorToEdit, setbookAuthorToEdit] = React.useState<BookAuthorEntity | null>(null);

    const handleChangePage = (_event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(+event.target.value);
        setPage(0);
    };

    const handleEdit = (book: BookAuthorEntity) => {
        setbookAuthorToEdit(book);
        setOpen(true);
    };

    const handleDelete = async (bookId: number, authorId: number) => {
        try {

            await deleteBookAuthor(bookId, authorId);
            onSuccess();
        } catch (error) {
            console.log(error);
        }
    };

    function handleClose(): void {
        setOpen(false);
        setbookAuthorToEdit(null);
        onSuccess();
    }


    const calculateTableHeight = () => {
        const rowHeight = 53;
        const headerHeight = 57;
        const paginationHeight = 52;

        const visibleRows = Math.max(2, Math.min(rowsPerPage, data.length));

        return headerHeight + (visibleRows * rowHeight) + paginationHeight;
    };

    return (
        <Paper sx={{ minWidth: '500px', overflow: 'hidden' }}>
            <EditBookAuthorModal open={open} onClose={handleClose} bookAuthorToEdit={bookAuthorToEdit} onSuccess={handleClose} />
            <TableContainer sx={{
                minHeight: calculateTableHeight(),
                maxHeight: 'calc(100vh - 200px)',
                minWidth: '500px'
            }}>
                <Table stickyHeader aria-label="sticky table" size="small">
                    <TableHead>
                        <TableRow>
                            {columns.map((column) => (
                                <TableCell
                                    key={column.id}
                                    align={column.align ?? 'left'}
                                    style={{
                                        top: 0,
                                        minWidth: column.minWidth,
                                        backgroundColor: '#f5f5f5'
                                    }}
                                >
                                    {column.label}
                                </TableCell>
                            ))}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {data.length > 0 ? (
                            data
                                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                .map((row) => (
                                    <TableRow hover role="checkbox" tabIndex={-1} key={`${row.bookId}-${row.authorId}`}>
                                        {columns.map((column) => {
                                            if (column.id === 'actions') {
                                                return (
                                                    <TableCell key={column.id} >
                                                        <IconButton onClick={() => handleEdit(row)} size="small">
                                                            <EditIcon fontSize="small" />
                                                        </IconButton>
                                                        <IconButton
                                                            onClick={() => handleDelete(row.bookId, row.authorId)}
                                                            color="error"
                                                            size="small"
                                                        >
                                                            <DeleteIcon fontSize="small" />
                                                        </IconButton>
                                                    </TableCell>
                                                );
                                            }
                                            const value = row[column.id as keyof BookAuthorEntity];
                                            return (
                                                <TableCell key={column.id} align={column.align ?? 'left'}>
                                                    {value}
                                                </TableCell>
                                            );
                                        })}
                                    </TableRow>
                                ))
                        ) : (
                            <TableRow>
                                <TableCell colSpan={columns.length} align="center">
                                    Nenhum dado disponível
                                </TableCell>
                            </TableRow>
                        )}
                    </TableBody>
                </Table>
            </TableContainer>
            <TablePagination
                rowsPerPageOptions={[10, 25, 100]}
                component="div"
                count={data.length}
                rowsPerPage={rowsPerPage}
                page={page}
                onPageChange={handleChangePage}
                onRowsPerPageChange={handleChangeRowsPerPage}
            />
        </Paper>
    );
}