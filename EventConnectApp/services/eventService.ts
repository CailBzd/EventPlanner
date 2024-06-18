import { AxiosResponse } from "axios";
import { axiosApiInstance } from "./axios";
import { Event } from '@/types/Event';

const API_URL = 'Event';

export const eventKeys = {

};

export const eventService = {
    getEvents,
    getEventById,
};

function getEvents(): Promise<AxiosResponse<Event[]>> {
    return axiosApiInstance.get(API_URL);
}

function getEventById(eventId: string): Promise<AxiosResponse<Event>> {
    return axiosApiInstance.get(API_URL + `/${eventId}`);
}