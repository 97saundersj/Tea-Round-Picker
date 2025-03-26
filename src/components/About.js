import React from 'react';
import { Container, Card, Row, Col } from 'react-bootstrap';

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

export default About; 