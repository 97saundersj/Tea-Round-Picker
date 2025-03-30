export interface CreateTeamDto {
    label?: string | null;
}

export interface Team {
    id: number;
    label?: string | null;
    participants?: Participant[] | null;
}

export interface Participant {
    id: number;
    teamId: number;
    name?: string | null;
    preferredTea?: string | null;
    team: Team;
}

export interface TeaOrder {
    id: number;
    teaRoundId: number;
    participantId: number;
    requestedTeaOrder?: string | null;
    teaRound: TeaRound;
    participant: Participant;
}

export interface TeaRound {
    id: number;
    teamId: number;
    chosenParticipantId: number;
    date: string;
    teaOrders?: TeaOrder[] | null;
    team: Team;
    chosenParticipant: Participant;
}