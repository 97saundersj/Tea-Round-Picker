import React, { useState, useEffect } from 'react';
import { Wheel } from 'react-custom-roulette';
import oilcanSvg from '../../images/oil-can.svg';
import { Modal, Button, Alert } from 'react-bootstrap';

const TeaWheel = ({ participants, showModal, onClose }) => {
  const [isSpinning, setIsSpinning] = useState(false);
  const [prizeNumber, setPrizeNumber] = useState(null);

  useEffect(() => {
    if (showModal) {
      const randomIndex = Math.floor(Math.random() * participants.length);
      setPrizeNumber(randomIndex);
      setIsSpinning(true);
    }
  }, [showModal, participants.length]);

  if (!participants || participants.length === 0) {
    return <p>No participants available.</p>;
  }

  const data = participants.map((participant) => ({ option: participant }));

  const handleSpinEnd = () => {
    setIsSpinning(false);
  };

  return (
    <Modal show={showModal} onHide={onClose} centered>
      <Modal.Header closeButton>
        <Modal.Title>Tea Wheel</Modal.Title>
      </Modal.Header>

      <Modal.Body className="text-center">
        <div style={{ display: 'flex', justifyContent: 'center', margin: '20px 0', position: 'relative' }}>
          <Wheel
            mustStartSpinning={isSpinning}
            prizeNumber={prizeNumber !== null ? prizeNumber : 0}
            data={data}
            onStopSpinning={handleSpinEnd}
            spinDuration={0.3}
            backgroundColors={['#FF6B6B', '#4ECDC4', '#45B7D1', '#96CEB4', '#FFEEAD', '#D4A5A5', '#9B59B6', '#3498DB']}
            textColors={['#FFFFFF']}
            outerBorderColor="#EEEEEE"
            outerBorderWidth={3}
            innerRadius={0}
            innerBorderColor="#FFFFFF"
            innerBorderWidth={2}
            radiusLineColor="#FFFFFF"
            radiusLineWidth={2}
            perpendicularText={true}
            textDistance={85}
            pointerProps={{
              src: oilcanSvg,
              style: {
                transform: 'scaleX(-1) rotate(45deg)',
                filter: 'drop-shadow(2px 2px 2px rgba(0,0,0,0.3))',
                transition: 'transform 0.2s ease-in-out',
              },
            }}
          />
        </div>

        {prizeNumber !== null && !isSpinning && (
          <Alert variant="success" className="text-center mt-3">
            🎉 {participants[prizeNumber]} will make the tea! 🎉
          </Alert>
        )}

        <Modal.Footer>
          {prizeNumber !== null && !isSpinning && (
            <Button 
              variant="primary" 
              onClick={() => {
                const randomIndex = Math.floor(Math.random() * participants.length);
                setPrizeNumber(randomIndex);
                setIsSpinning(true);
              }} 
            >
              Spin Again?
            </Button>
          )}
        </Modal.Footer>
      </Modal.Body>
    </Modal>
  );
};

export default TeaWheel;
