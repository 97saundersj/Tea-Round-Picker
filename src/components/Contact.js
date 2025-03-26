import React from 'react';
import { Container, Card, Row, Col } from 'react-bootstrap';

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

export default Contact; 