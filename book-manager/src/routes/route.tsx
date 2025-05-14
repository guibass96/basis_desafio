// src/routes.tsx
import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import App from '../App';
import Book from '../book/book';
import Author from '../author/author'
import Assunto from '../subject/Assunto';
import PurchaseOption from '../PurchaseOption/PurchaseOption';
import BookSubjects from '../bookSubject/BookSubjects';
import BookAuthor from '../bookAuthor/bookAuthor';





function Router() {
  return (

      <Routes>
        <Route path="/" element={<Book />} />
        <Route path="/books" element={<Book />} />
        <Route path="/authors" element={<Author />} />
        <Route path="/subject" element={<Assunto />} />
        <Route path="/optionsPurchase" element={<PurchaseOption />} />
        <Route path="/bookSubject" element={<BookSubjects  />} />
        <Route path="/bookAuthor" element={<BookAuthor  />} />
      </Routes>
  );
}

export default Router;
