import React from 'react';
import { Container, Card, Row, Col } from 'react-bootstrap';

const About = () => {
  return (
    <Container>
      <Row className="mt-4">
        <Col>
          <Card>
            <Card.Body>
              <Card.Title className="text-center">About</Card.Title>
              <Card.Text>
                TODO: link github
              </Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

export default About; 