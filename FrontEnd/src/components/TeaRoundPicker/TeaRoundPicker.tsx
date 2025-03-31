import React, { useState, useCallback } from 'react';
import TeaWheel from './TeaWheel';
import TeamSelector from './TeamSelector';
import ParticipantsList from './ParticipantsList';
import TeaRoundsTable from './TeaRoundsTable';
import { Team, Participant } from '../../types/Types';
import { api } from '../../services/api';
import { toast } from 'react-toastify';

const TeaRoundPicker: React.FC = () => {
  const [participants, setParticipants] = useState<Participant[]>([]);
  const [isSpinning, setIsSpinning] = useState<boolean>(false);
  const [selectedTeamId, setSelectedTeamId] = useState<number | null>(null);
  const [teams, setTeams] = useState<Team[]>([]);
  const [isLoadingTeams, setIsLoadingTeams] = useState<boolean>(false);
  const [refetchPreviousSelections, setRefetchPreviousSelections] = useState<boolean>(false);

  const fetchTeams = async (): Promise<void> => {
    setIsLoadingTeams(true);
    try {
      const response = await api.getTeams();
      setTeams(response);
    } catch (error) {
      console.error('Error fetching teams:', error);
      toast.error('Failed to fetch teams. Please try again.');
    } finally {
      setIsLoadingTeams(false);
    }
  };

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

  const handleTeamSelect = useCallback((team: Team | null): void => {
    if (team?.id) {
      fetchTeamById(team.id);
    } else {
      setParticipants([]);
      setSelectedTeamId(null);
    }
  }, []);

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
        teams={teams} 
        fetchTeams={fetchTeams}
        isLoading={isLoadingTeams}
      />
      
      <ParticipantsList 
        participants={participants} 
        setParticipants={setParticipants} 
        teamId={selectedTeamId} 
        onParticipantAdded={refetchTeam} 
      />
      
      <TeaWheel
        participants={participants}
        teamId={selectedTeamId}
        showModal={isSpinning}
        onClose={() => setIsSpinning(false)}
        pickedTeaMaker={pickedTeaMaker}
      />
      
      <TeaRoundsTable 
        teamId={selectedTeamId} 
        refresh={refetchPreviousSelections} 
      />
    </div>
  );
};

export default TeaRoundPicker; 