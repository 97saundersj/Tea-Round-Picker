import React, { useState } from 'react';
import CreatableSelect from 'react-select/creatable';
import { Team } from '../../types/Types';
import { api } from '../../services/api';
import { toast } from 'react-toastify';

export interface TeamSelectorProps {
  onTeamSelect: (team: Team | null) => void;
}

const TeamSelector: React.FC<TeamSelectorProps> = ({ onTeamSelect }) => {
  const [teams, setTeams] = useState<Team[]>([]);
  const [selectedTeam, setSelectedTeam] = useState<Team | null>(null);
  const [isLoadingTeams, setIsLoadingTeams] = useState<boolean>(false);

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

  const handleCreate = async (newValue: string) => {
    if (!newValue) {
      toast.error('Team name cannot be empty.');
      return;
    }

    const teamExists = teams.some(team => team.label?.toLowerCase() === newValue.toLowerCase());
    if (teamExists) {
      toast.error('This team already exists.');
      return;
    }

    const newTeam: Team = { label: newValue, participants: [] };

    try {
      const newCreatedTeam = await api.createTeam(newTeam);
      await fetchTeams();
      setSelectedTeam(newCreatedTeam);
      onTeamSelect(newCreatedTeam);
    } catch (error) {
      console.error('Error creating team:', error);
      toast.error('Error creating team. Please try again.');
    }
  };

  const handleChange = (newValue: Team | null) => {
    setSelectedTeam(newValue);
    onTeamSelect(newValue);
  };

  return (
    <div data-testid="team-selector" className="card m-2">
      <div className="card-body">
        <h5 className="card-title">Team</h5>
        <CreatableSelect<Team>
          data-testid="team-selector-input"
          isClearable
          isSearchable
          isLoading={isLoadingTeams}
          options={teams}
          value={selectedTeam}
          onChange={handleChange}
          onCreateOption={handleCreate}
          onMenuOpen={fetchTeams}
          getOptionValue={(option) => option?.id?.toString() || ''}
          getOptionLabel={(option) => option?.label || ''}
          placeholder="Enter a new team name or choose an existing one..."
        />
      </div>
    </div>
  );
};

export default TeamSelector;