import React, { useState } from 'react';
import { Wheel } from 'react-custom-roulette';
import { Modal, Button, Alert } from 'react-bootstrap';
import oilcanSvg from '../../images/oil-can.svg';
import { Participant } from '../../types/Types';
import { api } from '../../services/api';
import { toast } from 'react-toastify';

interface TeaWheelProps {
  participants: Participant[];
  teamId: number | null;
  pickedTeaMaker: () => void;
}

interface WheelData {
  option: string;
}

const TeaWheel: React.FC<TeaWheelProps> = ({
  participants = [],
  teamId,
  pickedTeaMaker
}) => {
  const [isSpinning, setIsSpinning] = useState<boolean>(false);
  const [selectedParticipant, setSelectedParticipant] = useState<Participant | null>(null);
  const [modalVisible, setModalVisible] = useState<boolean>(false);
  const [prizeNumber, setPrizeNumber] = useState<number>(0);

  // Create wheel data from participants
  const wheelData: WheelData[] = participants.length > 0
    ? participants.map(p => ({ option: p.name || 'Unknown' }))
    : [{ option: 'No participants' }];

  // Pick random tea maker
  const pickTeaMaker = async (): Promise<void> => {
    if (!teamId || participants.length === 0) return;

    try {
      const participantId = await api.addTeaRound(teamId);
      const selectedParticipant = participants.find(p => p.id === participantId);

      if (!selectedParticipant) throw new Error()

      setSelectedParticipant(selectedParticipant);

      const index = participants.findIndex(p => p.id === participantId);
      // Ensure index is valid before setting prize number
      setPrizeNumber(index >= 0 ? index : 0);
      setModalVisible(true);

      // Small delay to ensure state is updated
      setTimeout(() => setIsSpinning(true), 50);
    } catch (error) {
      console.error("Error picking tea maker:", error);
      toast.error("Error picking tea maker. Please try again.");
    }
  };

  // Wheel callbacks
  const handleSpinEnd = (): void => {
    setIsSpinning(false);
    pickedTeaMaker();
  };

  const handleClose = (): void => {
    setIsSpinning(false);
    setModalVisible(false);
  };

  if (!teamId) return null;

  return (
    <div className="card m-2">
      <div className="card-body">
        <h5 className="card-title">Pick Tea Maker</h5>
        <button
          className="btn btn-success btn-lg w-100 mb-3"
          onClick={pickTeaMaker}
          disabled={isSpinning || !participants || participants.length === 0}
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
              data={wheelData}
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
                },
              }}
            />
          </div>

          {selectedParticipant && !isSpinning && (
            <Alert variant="success" className="text-center mt-3">
              🎉 {selectedParticipant.name} will make the tea! 🎉
            </Alert>
          )}

          <Modal.Footer>
            {selectedParticipant && !isSpinning && (
              <Button
                variant="primary"
                onClick={pickTeaMaker}>
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