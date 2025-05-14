import React from 'react';
import { Link } from 'react-router-dom';
import { Drawer, List, ListItem, ListItemText, Typography, Box } from '@mui/material';

const SidebarMenu = () => {
  return (
    <Drawer
      variant="permanent"
      anchor="left"
      sx={{
        width: 240,
        flexShrink: 0,
        '& .MuiDrawer-paper': {
          width: 240,
          boxSizing: 'border-box',
        },
      }}
    >
      <Box sx={{ p: 2 }}>
        <Typography variant="h6" noWrap>
          Book Manager
        </Typography>
      </Box>
      <List>
        <ListItem  component={Link} to="/authors">
          <ListItemText primary="Autores" />
        </ListItem>
        <ListItem  component={Link} to="/books">
          <ListItemText primary="Livros" />
        </ListItem>
        <ListItem  component={Link} to="/subject">
          <ListItemText primary="Assuntos" />
        </ListItem>
      </List>
    </Drawer>
  );
};

export default SidebarMenu;
