import React, { useState } from 'react';
import TeaWheel from './TeaWheel';
import TeamSelector from './TeamSelector';

const TeaRoundPicker = () => {
  const [participants, setParticipants] = useState([]);
  const [newParticipant, setNewParticipant] = useState('');
  const [selectedTeaMaker] = useState(null);
  const [isSpinning, setIsSpinning] = useState(false);
  const [setSelectedTeam] = useState(null);

  const handleTeamSelect = (team) => {
    setSelectedTeam(team);
    setParticipants(team ? team.participants : []); // Set participants based on selected team
  };

  const handleAddParticipant = (e) => {
    e.preventDefault();
    if (newParticipant.trim() && !participants.includes(newParticipant.trim())) {
      setParticipants([...participants, newParticipant.trim()]);
      setNewParticipant('');
    }
  };

  const handleRemoveParticipant = (index) => {
    setParticipants(participants.filter((_, i) => i !== index));
  };

  const pickTeaMaker = () => {
    if (participants.length === 0) {
      alert('Please add at least one participant!');
      return;
    }

    setIsSpinning(true);
  };

  /*
  const handleSpinComplete = (winner) => {
    setIsSpinning(false);
    setSelectedTeaMaker(winner);
  };
  */

  const handleCloseTeaWheel = (isClosed) => {
    setIsSpinning(false);
  };

  return (
    <div className="container mt-5">
      <h1 className="mb-4">Nisien Tea Round Picker</h1>
      
      <TeamSelector onTeamSelect={handleTeamSelect} />
      
      <div className="card m-2">
        <div className="card-body">
          <h5 className="card-title">Participants</h5>
          <form onSubmit={handleAddParticipant} className="mb-3">
            <div className="input-group">
              <input
                type="text"
                className="form-control"
                value={newParticipant}
                onChange={(e) => setNewParticipant(e.target.value)}
                placeholder="Enter participant name"
              />
              <button type="submit" className="btn btn-primary">Add</button>
            </div>
          </form>

          <h6 className="card-subtitle mb-2">Current Participants:</h6>
          <ul className="list-group">
            {participants.map((participant, index) => (
              <li key={index} className="list-group-item d-flex justify-content-between align-items-center">
                {participant}
                <button
                  className="btn btn-sm btn-danger"
                  onClick={() => handleRemoveParticipant(index)}
                >
                  Remove
                </button>
              </li>
            ))}
          </ul>
        </div>
      </div>
      
      <div className="card m-2">
        <div className="card-body">
          <h5 className="card-title">Pick Tea Maker</h5>
          <button
            className="btn btn-success btn-lg w-100 mb-3"
            onClick={pickTeaMaker}
            disabled={isSpinning || participants.length === 0}
          >
            Pick Tea Maker
          </button>
          
          {participants.length > 0 && (
            <TeaWheel
              participants={participants}
              showModal={isSpinning}
              onClose={handleCloseTeaWheel}
              //onSpinComplete={handleSpinComplete}
            />
          )}
          
          {selectedTeaMaker && !isSpinning && (
            <div className="alert alert-success text-center mt-3">
              <h4 className="mb-0">🎉 {selectedTeaMaker} will make the tea! 🎉</h4>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default TeaRoundPicker; 