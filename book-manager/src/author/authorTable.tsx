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
import { AuthorEntity, deleteAuthor } from '../services/authorService';
import EditAuthorModal from './modal/editAuthorModal';


type ColumnId = keyof AuthorEntity | 'actions';

interface Column {
    id: ColumnId;
    label: string;
    minWidth?: number;
    align?: 'right' | 'left' | 'center';
}

const columns: Column[] = [
    { id: 'id', label: 'ID', minWidth: 50 },
    { id: 'name', label: 'Nome', minWidth: 100 },
    { id: 'actions', label: 'Ações', minWidth: 150 },
];

interface ColumnGroupingTableProps {
    data: AuthorEntity[];
    onSuccess: () => void;
}

export default function ColumnGroupingTable({ data, onSuccess }: ColumnGroupingTableProps) {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(10);
    const [open, setOpen] = React.useState(false);
    const [authorToEdit, setAuthorToEdit] = React.useState<AuthorEntity | null>(null);

    const handleChangePage = (_event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(+event.target.value);
        setPage(0);
    };

    const handleEdit = (author: AuthorEntity) => {
        setAuthorToEdit(author);
        setOpen(true);
    };

    const handleDelete = async (id: number) => {
        try {
            await deleteAuthor(id);
            console.log(` deletado com sucesso.`);
            onSuccess();
        } catch (error) {
            console.log('Erro');
        }
    };

    function handleClose(): void {
        setOpen(false);
        setAuthorToEdit(null);
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
        <Paper sx={{ width: '100%' }}>
            <EditAuthorModal open={open} onClose={handleClose} authorToEdit={authorToEdit} onSuccess={handleClose} />
            <TableContainer sx={{
                minHeight: calculateTableHeight(),
                maxHeight: 'calc(100vh - 200px)',
                minWidth: '500px',
                borderRadius: 2,
                boxShadow: '0 2px 8px rgba(0,0,0,0.05)',
                border: '1px solid #e0e0e0',
            }}>
                <Table stickyHeader aria-label="sticky table" sx={{ '& .MuiTableCell-root': { borderBottom: '1px solid #e0e0e0' } }}>
                    <TableHead sx={{ backgroundColor: '#f5f5f5' }}>
                        <TableRow>
                            {columns.map((column) => (
                                <TableCell
                                    key={column.id}
                                    align={column.align ?? 'left'}
                                    sx={{
                                        top: 57,
                                        minWidth: column.minWidth,
                                        fontWeight: 600,
                                        backgroundColor: '#fafafa',
                                        color: '#333',
                                    }}
                                >
                                    {column.label}
                                </TableCell>
                            ))}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {data
                            .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                            .map((row) => (
                                <TableRow
                                    hover
                                    tabIndex={-1}
                                    key={row.id}
                                    sx={{
                                        '&:hover': { backgroundColor: '#f9f9f9' },
                                        transition: 'background-color 0.3s',
                                    }}
                                >
                                    {columns.map((column) => {
                                        if (column.id === 'actions') {
                                            return (
                                                <TableCell key={column.id} >
                                                    <IconButton onClick={() => handleEdit(row)} size="small" sx={{ mr: 1 }}>
                                                        <EditIcon fontSize="small" />
                                                    </IconButton>
                                                    <IconButton onClick={() => handleDelete(row.id)} color="error" size="small">
                                                        <DeleteIcon fontSize="small" />
                                                    </IconButton>
                                                </TableCell>
                                            );
                                        }
                                        const value = row[column.id as keyof AuthorEntity];
                                        return (
                                            <TableCell key={column.id} align={column.align ?? 'left'}>
                                                {value}
                                            </TableCell>
                                        );
                                    })}
                                </TableRow>
                            ))}
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
