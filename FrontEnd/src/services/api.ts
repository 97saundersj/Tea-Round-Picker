import axios from 'axios';
import { Team, CreateTeamDto, TeaRound } from '../types/types';

const API_URL = process.env.REACT_APP_WEB_API_URL;

export const api = {
  getTeams: async (): Promise<Team[]> => {
    const response = await axios.get<Team[]>(`${API_URL}/teams`);
    return response.data;
  },

  getTeamById: async (teamId: number): Promise<Team> => {
    const response = await axios.get<Team>(`${API_URL}/teams/${teamId}`);
    return response.data;
  },

  createTeam: async (team: CreateTeamDto): Promise<Team> => {
    const response = await axios.post<Team>(`${API_URL}/teams`, team, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    return response.data;
  },

  addParticipant: async (teamId: number, participantName: string): Promise<void> => {
    await axios.post(
      `${API_URL}/participant/${teamId}`,
      participantName,
      {
        headers: {
          'Content-Type': 'application/json',
        },
      }
    );
  },

  removeParticipant: async (teamId: number, participantId: number): Promise<void> => {
    await axios.delete(`${API_URL}/teams/${teamId}/${participantId}`);
  },

  updatePreferredTea: async (participantId: number, preferredTea: string): Promise<void> => {
    await axios.put(
      `${API_URL}/participant/${participantId}`,
      preferredTea,
      {
        headers: {
          'Content-Type': 'application/json',
        },
      }
    );
  },

  // Tea round related API calls
  getRandomParticipant: async (teamId: number): Promise<string> => {
    const response = await axios.get<string>(`${API_URL}/participant/${teamId}/random`);
    return response.data;
  },

  getPreviousTeaRounds: async (teamId: number): Promise<TeaRound[]> => {
    const response = await axios.get<TeaRound[]>(`${API_URL}/teams/${teamId}/previous-participant-selections`);
    return response.data;
  }
}; 