import { AxiosResponse } from "axios";
import { axiosApiInstance } from "./axios";
import { Event } from '@/types/Event';

const API_URL = 'Events';

export const eventKeys = {

};

export const eventService = {
    getEvents,
    getEventById,
    createEvent,
};

function getEvents(token: string | null | undefined): Promise<AxiosResponse<Event[]>> {
    return axiosApiInstance.get(API_URL, {
        headers: {
            'accept': '*/*',
            'Authorization': `Bearer ${token}`,
        },
    });
}

function getEventById(eventId: string, token: string): Promise<AxiosResponse<Event>> {
    return axiosApiInstance.get(API_URL + `/` + eventId, {
        headers: {
            'accept': '*/*',
            'Authorization': `Bearer ${token}`,
        },
    });
}

async function createEvent(event: any, token: string | null): Promise<AxiosResponse<any>> {
    return axiosApiInstance.post(API_URL, event, {
        headers: { Authorization: `Bearer ${token}` },
    });
}