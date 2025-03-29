import React, { useState } from 'react';
import axios from 'axios';
import { Wheel } from 'react-custom-roulette';
import { Modal, Button, Alert } from 'react-bootstrap';
import oilcanSvg from '../../images/oil-can.svg';

const TeaWheel = ({ participants, teamId, showModal, onClose, pickedTeaMaker }) => {
  const [isSpinning, setIsSpinning] = useState(false);
  const [selectedParticipant, setSelectedParticipant] = useState(null);
  const [modalVisible, setModalVisible] = useState(showModal);
  const [prizeNumber, setPrizeNumber] = useState(0);

  const fetchRandomParticipant = async () => {
    try {
      const response = await axios.get(`${process.env.REACT_APP_WEB_API_URL}/participant/${teamId}/random`);
      const participant = response.data;
      
      setSelectedParticipant(participant);
      
      // Find the index of the selected participant in the participants list
      const index = participants.findIndex(p => p.name === participant.name);
      if (index !== -1) {
        setPrizeNumber(index);
      }
      
      setIsSpinning(true);
      setModalVisible(true);
    } catch (error) {
      console.error("Error fetching random participant:", error);
    }
  };

  const data = participants.map((participant) => ({ option: participant.name }));

  const handleSpinEnd = () => {
    setIsSpinning(false);
    if (pickedTeaMaker) {
      pickedTeaMaker();
    }
  };

  const handleClose = () => {
    setModalVisible(false);
  };

  if (!teamId) {
    return null; // Do not render the component if teamId is not defined
  }

  return (
    <div className="card m-2">
      <div className="card-body">
        <h5 className="card-title">Pick Tea Maker</h5>
        <button
          className="btn btn-success btn-lg w-100 mb-3"
          onClick={fetchRandomParticipant}
          disabled={isSpinning || participants.length === 0}
        >
          Pick Tea Maker
        </button>
      </div>
    
      <Modal show={modalVisible} onHide={handleClose} centered>
        <Modal.Header closeButton>
          <Modal.Title>Tea Wheel</Modal.Title>
        </Modal.Header>

        <Modal.Body className="text-center">
          <div style={{ display: 'flex', justifyContent: 'center', margin: '20px 0', position: 'relative' }}>
            <Wheel
              mustStartSpinning={isSpinning}
              prizeNumber={prizeNumber}
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

          {selectedParticipant && !isSpinning && (
            <Alert variant="success" className="text-center mt-3">
              🎉 {selectedParticipant} will make the tea! 🎉
            </Alert>
          )}

          <Modal.Footer>
            {selectedParticipant && !isSpinning && (
              <Button 
                variant="primary" 
                onClick={fetchRandomParticipant}
              >
                Spin Again?
              </Button>
            )}
          </Modal.Footer>
        </Modal.Body>
      </Modal>
    </div>
  );
};

export default TeaWheel;
