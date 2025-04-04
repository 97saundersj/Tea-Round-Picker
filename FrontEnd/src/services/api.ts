import axios from 'axios';
import { Team, TeaRound, Participant } from '../types/Types';

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

  createTeam: async (team: Team): Promise<Team> => {
    const response = await axios.post<Team>(`${API_URL}/teams`, team, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    return response.data;
  },

  getParticipantById: async (participantId: number): Promise<Participant> => {
    const response = await axios.get<Participant>(`${API_URL}/participant/${participantId}`);
    return response.data;
  },

  createParticipant: async (participant: Participant): Promise<Participant> => {
    const response = await axios.post<Participant>(
      `${API_URL}/participant`,
      participant,
      {
        headers: {
          'Content-Type': 'application/json',
        },
      }
    );
    return response.data;
  },

  addParticipantToTeam: async (teamId: number, participantName: string): Promise<void> => {
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

  addParticipantToTeamById: async (teamId: number, participantId: number): Promise<void> => {
    await axios.post(
      `${API_URL}/teams/${teamId}/participants/${participantId}`,
      null,
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

  updateParticipant: async (participant: Participant): Promise<void> => {
    await axios.put<Participant>(
      `${API_URL}/participant`,
      participant,
      {
        headers: {
          'Content-Type': 'application/json',
        },
      }
    );
  },

  getTeaRounds: async (teamId: number): Promise<TeaRound[]> => {
    const response = await axios.get<TeaRound[]>(`${API_URL}/teaRounds/${teamId}`);
    return response.data;
  },

  addTeaRound: async (teamId: number): Promise<number> => {
    const response = await axios.post<number>(`${API_URL}/teaRounds/${teamId}`);
    return response.data;
  },
}; 