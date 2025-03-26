import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { Container, Card, Row, Col } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

// Create basic components for different routes
const Home = () => {
  return (
    <Container>
      <Row className="mt-4">
        <Col>
          <Card>
            <Card.Body>
              <Card.Title className="text-center">Welcome to Our Website</Card.Title>
              <Card.Text>
                This is a modern React application built with React Router and Bootstrap.
                Feel free to explore the different pages using the navigation menu above.
              </Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

const About = () => {
  return (
    <Container>
      <Row className="mt-4">
        <Col>
          <Card>
            <Card.Body>
              <Card.Title className="text-center">About Us</Card.Title>
              <Card.Text>
                We are a passionate team dedicated to creating amazing web experiences.
                Our mission is to build innovative solutions that make a difference.
              </Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

const Contact = () => {
  return (
    <Container>
      <Row className="mt-4">
        <Col>
          <Card>
            <Card.Body>
              <Card.Title className="text-center">Contact Us</Card.Title>
              <Card.Text>
                Get in touch with us! We'd love to hear from you.
                Email: contact@example.com
              </Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

const App = () => {
  return (
    <Router>
      <div>
        {/* Simple Navigation */}
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark mb-4">
          <div className="container">
            <Link className="navbar-brand" to="/">My Website</Link>
            <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarNav">
              <ul className="navbar-nav ms-auto">
                <li className="nav-item">
                  <Link className="nav-link" to="/">Home</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/about">About</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/contact">Contact</Link>
                </li>
              </ul>
            </div>
          </div>
        </nav>

        {/* Routes */}
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/about" element={<About />} />
          <Route path="/contact" element={<Contact />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;