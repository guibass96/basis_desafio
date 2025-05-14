import * as React from 'react';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { deletePurchaseOption, PurchaseOptionEntity } from '../services/PurchaseOption';
import { EditCreatePurchaseOptionModal } from './modal/editPurchaseOptionModal';

type ColumnId = keyof PurchaseOptionEntity | 'actions';

interface Column {
  id: ColumnId;
  label: string;
  minWidth?: number;
  align?: 'right' | 'left' | 'center';
}

const columns: Column[] = [
  { id: 'id', label: 'ID', minWidth: 50 },
  { id: 'nameBook', label: 'Nome Livro', minWidth: 80 },
  { id: 'price', label: 'Preço', minWidth: 100 },
  { id: 'saleCategory', label: 'Tipo Venda', minWidth: 150 },
  { id: 'actions', label: 'Ações', minWidth: 150, align: 'center' },
];

interface ColumnGroupingTableProps {
  data: PurchaseOptionEntity[];
  onSuccess: () => void;
}

export default function ColumnGroupingTable({ data, onSuccess }: ColumnGroupingTableProps) {
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);
  const [open, setOpen] = React.useState(false);
  const [authorToEdit, setAuthorToEdit] = React.useState<PurchaseOptionEntity | null>(null);

  const handleChangePage = (_event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  const handleEdit = (book: PurchaseOptionEntity) => {
    setAuthorToEdit(book);
    setOpen(true);
  };

  const handleDelete = async (id: number) => {
    try {
      await deletePurchaseOption(id);
      onSuccess();
    } catch (error) {
      console.log(error);
    }
  };

  const handleClose = () => {
    setOpen(false);
    setAuthorToEdit(null);
    onSuccess();
  };

  return (
    <Paper sx={{ width: '100%', overflowX: 'auto', borderRadius: 2, boxShadow: 3, p: 2 }}>
      <EditCreatePurchaseOptionModal
        open={open}
        onClose={handleClose}
        purchaseToEdit={authorToEdit}
        onSuccess={handleClose}
      />
      <TableContainer sx={{ minWidth: '100%' }}>
        <Table stickyHeader>
          <TableHead>
            <TableRow>
              {columns.map((column) => (
                <TableCell
                  key={column.id}
                  align={column.align ?? 'left'}
                  sx={{ fontWeight: 'bold', backgroundColor: '#f5f5f5' }}
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
                <TableRow hover key={row.id}>
                  {columns.map((column) => {
                    if (column.id === 'actions') {
                      return (
                        <TableCell key={column.id} align="center">
                          <div style={{ display: 'flex', justifyContent: 'center', gap: '0.5rem' }}>
                            <IconButton onClick={() => handleEdit(row)} size="small">
                              <EditIcon fontSize="small" />
                            </IconButton>
                            <IconButton onClick={() => handleDelete(row.id)} color="error" size="small">
                              <DeleteIcon fontSize="small" />
                            </IconButton>
                          </div>
                        </TableCell>
                      );
                    }
                    const value = row[column.id as keyof PurchaseOptionEntity];
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
