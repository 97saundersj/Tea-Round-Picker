import React, { useState, FormEvent } from 'react';
import ParticipantItem from './ParticipantItem';
import { Participant } from '../../types/Types';
import { api } from '../../services/api';
import { toast } from 'react-toastify';

interface ParticipantsListProps {
  teamId: number | null;
  participants: Participant[];
  setParticipants: React.Dispatch<React.SetStateAction<Participant[]>>;
  onParticipantAdded: () => void;
}

const ParticipantsList: React.FC<ParticipantsListProps> = ({
  teamId,
  participants,
  setParticipants,
  onParticipantAdded
}) => {
  const [newParticipant, setNewParticipant] = useState<string>('');

  const addParticipantToAPI = async (participantName: string): Promise<void> => {
    if (!teamId) return;

    var participant: Participant = {
      id: 0,
      name: participantName,
      teamId: teamId,
      preferredTea: "",
    }

    try {
      const createdParticipant = await api.createParticipant(participant);
      if (!createdParticipant || !createdParticipant.id) throw new Error("Participant invalid");
      await api.addParticipantToTeamById(teamId, createdParticipant.id);
    } catch (error) {
      console.error("Error adding participant:", error);
      toast.error('Error adding participant. Please try again.');
    }
  };

  const handleAddParticipant = async (e: FormEvent<HTMLFormElement>): Promise<void> => {
    e.preventDefault();
    const trimmedName = newParticipant.trim();

    if (trimmedName && !participants.some(p => p.name === trimmedName)) {
      await addParticipantToAPI(trimmedName);

      const newParticipant: Participant = {
        teamId: teamId!,
        name: trimmedName,
        preferredTea: null,
      };

      setParticipants([...participants, newParticipant]);
      setNewParticipant('');
      onParticipantAdded();
    }
  };

  const handleRemoveParticipant = async (index: number): Promise<void> => {
    if (!teamId) return;

    const participantToRemove = participants[index];
    if (!participantToRemove || !participantToRemove.id) return;

    try {
      await api.removeParticipant(teamId, participantToRemove.id);
      setParticipants(participants.filter((_, i) => i !== index));
    } catch (error) {
      console.error("Error removing participant:", error);
      toast.error('Error removing participant. Please try again.');
    }
  };

  const handlePreferredTeaChange = async (id: number | null | undefined, newTea: string): Promise<void> => {
    try {
      if (!id) throw new Error("Participant invalid");

      // Fetch the participant to update
      const participantToUpdate = participants.find(participant => participant.id === id);
      if (!participantToUpdate) throw new Error("Participant not found");

      // Update the preferredTea property
      const updatedParticipant = { ...participantToUpdate, preferredTea: newTea };

      await api.updateParticipant(updatedParticipant); // Pass the entire participant object

      setParticipants(participants.map((participant) =>
        participant.id === id ? updatedParticipant : participant
      ));

    } catch (error) {
      console.error("Error updating preferred tea:", error);
      toast.error('Error updating preferred tea. Please try again.');
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