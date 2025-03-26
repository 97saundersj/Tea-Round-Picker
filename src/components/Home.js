import React from 'react';
import { Container, Card, Row, Col } from 'react-bootstrap';

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

export default Home; 