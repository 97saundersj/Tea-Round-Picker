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
              <Card.Text className="text-center">
                Check out the project on <a href="https://github.com/97saundersj/Tea-Round-Picker" target="_blank" rel="noopener noreferrer">GitHub</a>.
              </Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

export default About;