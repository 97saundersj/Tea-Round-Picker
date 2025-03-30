import React, { useState, useEffect, useCallback } from 'react';
import axios from 'axios';
import TeaWheel from './TeaWheel';
import TeamSelector from './TeamSelector';
import ParticipantsList from './ParticipantsList';
import TeaRoundsTable from './TeaRoundsTable';
import { Team, Participant, TeamsResponse } from '../../types/Types';

const TeaRoundPicker: React.FC = () => {
  const [participants, setParticipants] = useState<Participant[]>([]);
  const [isSpinning, setIsSpinning] = useState<boolean>(false);
  const [selectedTeamId, setSelectedTeamId] = useState<number | null>(null);
  const [teams, setTeams] = useState<Team[]>([]);
  const [refetchPreviousSelections, setRefetchPreviousSelections] = useState<boolean>(false);

  const fetchTeams = async (): Promise<void> => {
    try {
      const response = await axios.get<TeamsResponse>(`${process.env.REACT_APP_WEB_API_URL}/teams`);
      setTeams(response.data);
    } catch (error) {
      console.error('Error fetching teams:', error);
    }
  };

  const fetchTeamById = async (teamId: number): Promise<void> => {
    try {
      const response = await axios.get<Team>(`${process.env.REACT_APP_WEB_API_URL}/teams/${teamId}`);
      setParticipants(response.data.participants || []);
      setSelectedTeamId(response.data.id);
    } catch (error) {
      console.error('Error fetching team:', error);
    }
  };

  const handleTeamSelect = useCallback((team: Team | null): void => {
    if (team) {
      fetchTeamById(team.id);
    } else {
      setParticipants([]);
      setSelectedTeamId(null);
    }
  }, []);

  useEffect(() => {
    fetchTeams();
  }, []);

  const pickedTeaMaker = (): void => {
    // Your logic for picking a tea goes here
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