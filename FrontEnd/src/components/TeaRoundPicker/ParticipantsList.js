import React, { useState } from 'react';
import axios from 'axios'; // Import Axios

const ParticipantsList = ({ participants, setParticipants, teamId, onParticipantAdded }) => {
  const [newParticipant, setNewParticipant] = useState('');
  const [errorMessage, setErrorMessage] = useState(''); // State for error messages

  const addParticipantToAPI = async (participantName) => {
    try {
      const response = await axios.post(`${process.env.REACT_APP_WEB_API_URL}/teams/${teamId}/participants`, participantName, {
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (response.status !== 204) {
        throw new Error('Failed to add participant');
      }
    } catch (error) {
      console.error("Error adding participant:", error);
      setErrorMessage('Error adding participant. Please try again.'); // Set error message
    }
  };

  const handleAddParticipant = async (e) => {
    e.preventDefault();
    const trimmedName = newParticipant.trim();
    if (trimmedName && !participants.includes(trimmedName)) {
      await addParticipantToAPI(trimmedName); // Call the new function
      setParticipants([...participants, trimmedName]);
      setNewParticipant('');
      setErrorMessage(''); // Clear error message on successful addition
      onParticipantAdded();
    }
  };

  const handleRemoveParticipant = async (index) => {
    const participantToRemove = participants[index].name;
    try {
      const response = await axios.delete(`${process.env.REACT_APP_WEB_API_URL}/teams/${teamId}/participants/${participantToRemove}`);

      if (response.status !== 204) {
        throw new Error('Failed to remove participant');
      }

      setParticipants(participants.filter((_, i) => i !== index)); // Update local state
    } catch (error) {
      console.error("Error removing participant:", error.response ? error.response.data : error.message); // Log detailed error response
      setErrorMessage('Error removing participant. Please try again.'); // Set error message
    }
  };

  if (!teamId) {
    return null; // Do not render the component if teamId is not defined
  }

  return (
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
        {errorMessage && <div className="text-danger">{errorMessage}</div>} {/* Display error message */}
        <h6 className="card-subtitle mb-2">Current Participants:</h6>
        <ul className="list-group">
          {participants.map((participant, index) => (
            <li key={index} className="list-group-item d-flex justify-content-between align-items-center">
              {participant.name}
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
  );
};

export default ParticipantsList; 