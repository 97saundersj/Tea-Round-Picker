import React, { useState } from 'react';
import TeaWheel from './TeaWheel';
import TeamSelector from './TeamSelector';
import ParticipantsList from './ParticipantsList';
import TeaRoundsTable from './TeaRoundsTable';
import { Team, Participant } from '../../types/Types';
import { api } from '../../services/api';
import { toast } from 'react-toastify';

const TeaRoundPickerPage: React.FC = () => {
  const [participants, setParticipants] = useState<Participant[]>([]);
  const [selectedTeamId, setSelectedTeamId] = useState<number | null>(null);
  const [refetchPreviousSelections, setRefetchPreviousSelections] = useState<boolean>(false);

  const fetchTeamById = async (teamId: number): Promise<void> => {
    try {
      const response = await api.getTeamById(teamId);
      setParticipants(response.participants || []);
      if (response.id) {
        setSelectedTeamId(response.id);
      }
    } catch (error) {
      console.error('Error fetching team:', error);
      toast.error('Failed to fetch team details. Please try again.');
    }
  };

  const handleTeamSelect = (team: Team | null): void => {
    if (team?.id) {
      fetchTeamById(team.id);
    } else {
      setParticipants([]);
      setSelectedTeamId(null);
    }
  };

  const pickedTeaMaker = (): void => {
    setRefetchPreviousSelections(prev => !prev);
  };

  const refetchTeam = (): void => {
    if (selectedTeamId) {
      fetchTeamById(selectedTeamId);
    }
  };

  return (
    <div className="container">
      <h1 className="mb-4">Tea Round Picker</h1>

      <TeamSelector
        onTeamSelect={handleTeamSelect}
      />

      <ParticipantsList
        teamId={selectedTeamId}
        participants={participants}
        setParticipants={setParticipants}
        onParticipantAdded={refetchTeam}
      />

      <TeaWheel
        teamId={selectedTeamId}
        participants={participants}
        pickedTeaMaker={pickedTeaMaker}
      />

      <TeaRoundsTable
        teamId={selectedTeamId}
        refresh={refetchPreviousSelections}
      />
    </div>
  );
};

export default TeaRoundPickerPage;