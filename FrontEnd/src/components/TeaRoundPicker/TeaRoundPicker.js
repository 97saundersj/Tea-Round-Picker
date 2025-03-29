import React, { useState, useEffect, useCallback } from 'react';
import axios from 'axios';
import TeaWheel from './TeaWheel';
import TeamSelector from './TeamSelector';
import ParticipantsList from './ParticipantsList';
import TeaRoundsTable from './TeaRoundsTable';

const TeaRoundPicker = () => {
  const [participants, setParticipants] = useState([]);
  const [isSpinning, setIsSpinning] = useState(false);
  const [selectedTeamId, setSelectedTeamId] = useState(null);
  const [teams, setTeams] = useState([]);
  const [refetchPreviousSelections, setRefetchPreviousSelections] = useState(false);

  const fetchTeams = async () => {
    try {
      const response = await axios.get(`${process.env.REACT_APP_WEB_API_URL}/teams`);
      setTeams(response.data);
    } catch (error) {
      console.error('Error fetching teams:', error);
    }
  };

  const fetchTeamById = async (teamId) => {
    try {
      const response = await axios.get(`${process.env.REACT_APP_WEB_API_URL}/teams/${teamId}`);
      setParticipants(response.data.participants);
      setSelectedTeamId(response.data.id);
    } catch (error) {
      console.error('Error fetching team:', error);
    }
  };

  const handleTeamSelect = useCallback((team) => {
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

  const pickedTeaMaker = () => {
    // Your logic for picking a tea goes here

    // Trigger a refetch of previous selections
    setRefetchPreviousSelections(prev => !prev);
  };

  const refetchTeam = () => {
    if (selectedTeamId) {
      fetchTeamById(selectedTeamId);
    }
  };

  return (
    <div className="container">
      <h1 className="mb-4">Tea Round Picker</h1>
      
      <TeamSelector onTeamSelect={handleTeamSelect} teams={teams} setTeams={setTeams} fetchTeams={fetchTeams} />
      
      <ParticipantsList participants={participants} setParticipants={setParticipants} teamId={selectedTeamId} onParticipantAdded={refetchTeam} />
      
      <TeaWheel
        participants={participants}
        teamId={selectedTeamId}
        showModal={isSpinning}
        onClose={() => setIsSpinning(false)}
        pickedTeaMaker={pickedTeaMaker}
      />
      
      <TeaRoundsTable teamId={selectedTeamId} refresh={refetchPreviousSelections} />
    </div>
  );
};

export default TeaRoundPicker; 