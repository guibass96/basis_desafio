import React from 'react';
import { Link, Routes, Route } from 'react-router-dom';
import { Box, Typography, AppBar, Toolbar, Drawer, List, ListItem, ListItemText, CssBaseline } from '@mui/material';
import Routa from '../src/routes/route';

const drawerWidth = 240;

function App() {
  return (
    <Box sx={{ display: 'flex' }}>
      <CssBaseline />

      <AppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1, backgroundColor: '#1976d2' }}>
        <Toolbar>
          <Typography variant="h6" noWrap component="div">
            Gerenciador de Livros
          </Typography>
        </Toolbar>
      </AppBar>

      <Drawer
        variant="permanent"
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          [`& .MuiDrawer-paper`]: { width: drawerWidth, boxSizing: 'border-box' },
        }}
      >
        <Toolbar />
        <Box sx={{ overflow: 'auto' }}>
          <List>
            <ListItem
              component={Link}
              to="/"
              sx={{
                textDecoration: 'none',
                '&:hover': {
                  backgroundColor: '#f0f0f0', 
                },
              }}
            >
              <ListItemText
                primary="Início"
                sx={{
                  color: '#1976d2', 
                  fontWeight: 'bold', 
                  '&:hover': {
                    color: '#0d47a1', 
                  },
                }}
              />
            </ListItem>
            <ListItem
              component={Link}
              to="/authors"
              sx={{
                textDecoration: 'none',
                '&:hover': {
                  backgroundColor: '#f0f0f0', 
                },
              }}
            >
              <ListItemText
                primary="Autores"
                sx={{
                  color: '#1976d2', 
                  fontWeight: 'bold', 
                  '&:hover': {
                    color: '#0d47a1', 
                  },
                }}
              />
            </ListItem>
            <ListItem
              component={Link}
              to="/books"
              sx={{
                textDecoration: 'none',
                '&:hover': {
                  backgroundColor: '#f0f0f0', 
                },
              }}
            >
              <ListItemText
                primary="Livros"
                sx={{
                  color: '#1976d2', 
                  fontWeight: 'bold', 
                  '&:hover': {
                    color: '#0d47a1', 
                  },
                }}
              />
            </ListItem>
            <ListItem
              component={Link}
              to="/subject"
              sx={{
                textDecoration: 'none',
                '&:hover': {
                  backgroundColor: '#f0f0f0', 
                },
              }}
            >
              <ListItemText
                primary="Assuntos"
                sx={{
                  color: '#1976d2', 
                  fontWeight: 'bold', 
                  '&:hover': {
                    color: '#0d47a1', 
                  },
                }}
              />
            </ListItem>
            <ListItem
              component={Link}
              to="/optionsPurchase"
              sx={{
                textDecoration: 'none',
                '&:hover': {
                  backgroundColor: '#f0f0f0', 
                },
              }}
            >
              <ListItemText
                primary="Opções de Compra"
                sx={{
                  color: '#1976d2', 
                  fontWeight: 'bold', 
                  '&:hover': {
                    color: '#0d47a1', 
                  },
                }}
              />
            </ListItem>
            <ListItem
              component={Link}
              to="/bookSubject"
              sx={{
                textDecoration: 'none',
                '&:hover': {
                  backgroundColor: '#f0f0f0', 
                },
              }}
            >
              <ListItemText
                primary="Livro Assunto"
                sx={{
                  color: '#1976d2', 
                  fontWeight: 'bold', 
                  '&:hover': {
                    color: '#0d47a1', 
                  },
                }}
              />
            </ListItem>
            <ListItem
              component={Link}
              to="/bookAuthor"
              sx={{
                textDecoration: 'none',
                '&:hover': {
                  backgroundColor: '#f0f0f0', 
                },
              }}
            >
              <ListItemText
                primary="Livro Autor"
                sx={{
                  color: '#1976d2', 
                  fontWeight: 'bold',
                  '&:hover': {
                    color: '#0d47a1', 
                  },
                }}
              />
            </ListItem>
          </List>
        </Box>
      </Drawer>

      <Box component="main" sx={{ flexGrow: 1, bgcolor: 'background.default', p: 3 }}>
        <Toolbar />
        <Routa />
      </Box>
    </Box>
  );
}

export default App;
