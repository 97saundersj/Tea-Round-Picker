import React, { useState, FormEvent } from 'react';
import axios from 'axios';
import ParticipantItem from './ParticipantItem';
import { Participant, Team } from '../../types/Types';

interface ParticipantsListProps {
  participants: Participant[];
  setParticipants: React.Dispatch<React.SetStateAction<Participant[]>>;
  teamId: number | null;
  onParticipantAdded: () => void;
}

const ParticipantsList: React.FC<ParticipantsListProps> = ({ 
  participants, 
  setParticipants, 
  teamId, 
  onParticipantAdded 
}) => {
  const [newParticipant, setNewParticipant] = useState<string>('');
  const [errorMessage, setErrorMessage] = useState<string>('');

  const addParticipantToAPI = async (participantName: string): Promise<void> => {
    if (!teamId) return;
    
    try {
      const response = await axios.post(
        `${process.env.REACT_APP_WEB_API_URL}/participant/${teamId}`, 
        participantName,
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );

      if (response.status !== 204) {
        throw new Error('Failed to add participant');
      }
    } catch (error) {
      console.error("Error adding participant:", error);
      setErrorMessage('Error adding participant. Please try again.');
    }
  };

  const handleAddParticipant = async (e: FormEvent<HTMLFormElement>): Promise<void> => {
    e.preventDefault();
    const trimmedName = newParticipant.trim();
    
    if (trimmedName && !participants.some(p => p.name === trimmedName)) {
      await addParticipantToAPI(trimmedName);
      const newParticipantObj: Participant = {
        id: 0, // This will be set by the server
        teamId: teamId!,
        name: trimmedName,
        preferredTea: null,
        team: {} as Team // This will be populated by the server
      };
      setParticipants([...participants, newParticipantObj]);
      setNewParticipant('');
      setErrorMessage('');
      onParticipantAdded();
    }
  };

  const handleRemoveParticipant = async (index: number): Promise<void> => {
    if (!teamId) return;
    
    const participantToRemove = participants[index];
    try {
      const response = await axios.delete(
        `${process.env.REACT_APP_WEB_API_URL}/teams/${teamId}/${participantToRemove.id}`
      );

      if (response.status !== 204) {
        throw new Error('Failed to remove participant');
      }

      setParticipants(participants.filter((_, i) => i !== index));
    } catch (error) {
      console.error("Error removing participant:", error);
      setErrorMessage('Error removing participant. Please try again.');
    }
  };

  const handlePreferredTeaChange = async (id: number, newTea: string): Promise<void> => {
    try {
      const response = await axios.put(
        `${process.env.REACT_APP_WEB_API_URL}/participant/${id}`, 
        newTea,
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );

      if (response.status !== 204) {
        throw new Error('Failed to update preferred tea');
      }

      setParticipants(participants.map((participant) =>
        participant.id === id ? { ...participant, preferredTea: newTea } : participant
      ));
    } catch (error) {
      console.error("Error updating preferred tea:", error);
      setErrorMessage('Error updating preferred tea. Please try again.');
    }
  };

  if (!teamId) {
    return null;
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
        {errorMessage && <div className="text-danger">{errorMessage}</div>}
        <h6 className="card-subtitle mb-2">Current Participants:</h6>
        <ul className="list-group">
          {participants.map((participant, index) => (
            <ParticipantItem
              key={participant.id}
              participant={participant}
              index={index}
              handlePreferredTeaChange={handlePreferredTeaChange}
              handleRemoveParticipant={handleRemoveParticipant}
            />
          ))}
        </ul>
      </div>
    </div>
  );
};

export default ParticipantsList; 