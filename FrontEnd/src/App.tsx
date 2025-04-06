import React from 'react';
import { HashRouter as Router, Routes, Route } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

// Import components
import Navbar from './components/Navbar';
import About from './components/About';
import TeaRoundPickerPage from './components/TeaRoundPicker/TeaRoundPickerPage';

const App: React.FC = () => {
  return (
    <Router>
      <div data-testid="app-container">
        <Navbar />
        <Routes>
          <Route path="/" element={<TeaRoundPickerPage />} />
          <Route path="/about" element={<About />} />
        </Routes>
        <ToastContainer position="top-right" />
      </div>
    </Router>
  );
};

export default App; 