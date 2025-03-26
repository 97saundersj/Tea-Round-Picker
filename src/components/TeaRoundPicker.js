import React, { useState } from 'react';

const TeaRoundPicker = () => {
  const [participants, setParticipants] = useState([]);
  const [newParticipant, setNewParticipant] = useState('');
  const [selectedTeaMaker, setSelectedTeaMaker] = useState(null);

  const handleAddParticipant = (e) => {
    e.preventDefault();
    if (newParticipant.trim()) {
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
    const randomIndex = Math.floor(Math.random() * participants.length);
    setSelectedTeaMaker(participants[randomIndex]);
  };

  return (
    <div className="container mt-5">
      <h1 className="mb-4">Nisien Tea Round Picker</h1>
      
      <div className="row">
        <div className="col-md-6">
          <div className="card">
            <div className="card-body">
              <h5 className="card-title">Add Participants</h5>
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
        </div>

        <div className="col-md-6">
          <div className="card">
            <div className="card-body">
              <h5 className="card-title">Pick Tea Maker</h5>
              <button
                className="btn btn-success btn-lg w-100 mb-3"
                onClick={pickTeaMaker}
              >
                Pick Tea Maker
              </button>
              
              {selectedTeaMaker && (
                <div className="alert alert-success text-center">
                  <h4 className="mb-0">🎉 {selectedTeaMaker} will make the tea! 🎉</h4>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TeaRoundPicker; 