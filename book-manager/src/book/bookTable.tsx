import * as React from 'react';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import { BookEntity, deleteBook } from '../services/bookService';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from '@mui/material/IconButton';
import EditBookModal from './modal/EditBookModal';

type ColumnId = keyof BookEntity | 'actions';

interface Column {
  id: ColumnId;
  label: string;
  minWidth?: number;
  align?: 'right' | 'left' | 'center';
}

const columns: Column[] = [
  { id: 'id', label: 'ID', minWidth: 50 },
  { id: 'title', label: 'Título', minWidth: 100 },
  { id: 'publisher', label: 'Editora', minWidth: 150 },
  { id: 'edition', label: 'Edição', minWidth: 80 },
  { id: 'publicationYear', label: 'Ano de Publicação', minWidth: 120 },
  { id: 'actions', label: 'Ações', minWidth: 150, align: 'center' },
];

interface ColumnGroupingTableProps {
  data: BookEntity[];
  onSuccess: () => void;
}

export default function ColumnGroupingTable({ data, onSuccess }: ColumnGroupingTableProps) {
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);
  const [open, setOpen] = React.useState(false);
  const [bookToEdit, setBookToEdit] = React.useState<BookEntity | null>(null);

  const handleChangePage = (_event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  const handleEdit = (book: BookEntity) => {
    setBookToEdit(book);
    setOpen(true);
  };

  const handleDelete = async (id: number) => {
    try {
      await deleteBook(id);
      onSuccess();
    } catch (error) {
      console.log(error);
    }
  };

  function handleClose(): void {
    setOpen(false);
    setBookToEdit(null);
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
    <Paper sx={{ width: '100%', overflow: 'hidden' }}>
      <EditBookModal open={open} onClose={handleClose} bookToEdit={bookToEdit} onSuccess={handleClose} />

      <TableContainer
        sx={{
          minHeight: calculateTableHeight(),
          maxHeight: 'calc(100vh - 200px)',
          minWidth: '600px',
        }}
      >
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              {columns.map((column) => (
                <TableCell
                  key={column.id}
                  align={column.align ?? 'left'}
                  sx={{ top: 57, minWidth: column.minWidth, backgroundColor: '#f5f5f5', fontWeight: 'bold' }}
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
                <TableRow hover tabIndex={-1} key={row.id} sx={{ cursor: 'pointer' }}>
                  {columns.map((column) => {
                    if (column.id === 'actions') {
                      return (
                        <TableCell key={column.id} align="center">
                          <IconButton onClick={() => handleEdit(row)} size="small">
                            <EditIcon fontSize="small" />
                          </IconButton>
                          <IconButton onClick={() => handleDelete(row.id)} color="error" size="small">
                            <DeleteIcon fontSize="small" />
                          </IconButton>
                        </TableCell>
                      );
                    }
                    const value = row[column.id as keyof BookEntity];
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
