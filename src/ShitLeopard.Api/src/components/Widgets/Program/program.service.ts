import axios, { AxiosPromise } from 'axios';
import { Helper } from '@/services';
import {
  Affiliation,
  Organization,
  PlayLevel,
  PlayType,
  Season,
  Tier1District,
  Tier2League,
  Tier3Club
} from './models';
const helper = new Helper();

export class ProgramService {
  public programTypes = [
    { id: '1', name: 'Tryout' },
    { id: '2', name: 'Non-Tryout' },
    { id: '3', name: 'Camp' },
    { id: '4', name: 'Team Coach Registration - Youth' }
  ];
  public activityTypes = [
    { id: 1, name: 'Baseball' },
    { id: 2, name: 'Basketball' },
    { id: 3, name: 'Football' },
    { id: 4, name: 'Hockey' },
    { id: 5, name: 'Lacrosse' },
    { id: 6, name: 'Soccer' },
    { id: 7, name: 'Softball' },
    { id: 8, name: 'Race' },
    { id: 9, name: 'Other' },
    { id: 10, name: 'Golf' },
    { id: 11, name: 'Tennis' },
    { id: 12, name: 'Volleyball' },
    { id: 13, name: 'Baseball/Softball' },
    { id: 14, name: 'Cheer' },
    { id: 15, name: 'Rugby' },
    { id: 16, name: 'Flag Football' }
  ];
  private isBusy = false;
  public get busy() {
    return this.isBusy;
  }
  public getActivityTypes() {}
  public getSportsAffiliations(
    sportsAffiliationId: number = null
  ): Promise<Affiliation[]> {
    return this.dispatchRequest<Affiliation[]>(`/ngb/api/v1/Admin/Providers`);
  }
  public getOrganizations(
    ngbid: any,
    activityType: number = null
  ): Promise<Organization[]> {
    return this.dispatchRequest<Organization[]>(
      `/ngb/api/v1/${ngbid}/Organizations?activityType=${activityType}`
    );
  }
  public getTier1Districts(
    ngbid: any,
    organizationId: string
  ): Tier1District[] {
    return null;
  }
  public getTier2Leagues(
    ngbid: any,
    organizationId: string
  ): Promise<Tier2League[]> {
    return this.dispatchRequest<Tier2League[]>(
      `/ngb/api/v1/${ngbid}/Tier2/${organizationId}`
    ).then(x => {
      return x.filter(y => y.name.length > 1);
    });
  }
  public getTier2LeaguesByDistrict(
    ngbid: any,
    organizationId: string,
    tier1DistrictId: string
  ): Promise<Tier2League[]> {
    return this.dispatchRequest<Tier2League[]>(
      `/ngb/api/v1/${ngbid}/Tier2/${organizationId}/Tier1/${tier1DistrictId}`
    );
  }
  public getTier3Clubs(
    ngbid: any,
    tier2LeagueId: string
  ): Promise<Tier3Club[]> {
    return this.dispatchRequest<Tier3Club[]>(
      `/ngb/api/v1/${ngbid}/Tier3/${tier2LeagueId}`
    );
  }
  public getSeasons(ngbid: any, tier2LeagueId: string): Promise<Season[]> {
    return this.dispatchRequest<Season[]>(
      `/ngb/api/v1/${ngbid}/Seasons/${tier2LeagueId}`
    );
  }
  public getPlayLevels(
    ngbid: any,
    tier2LeagueId: string,
    seasonId: string
  ): Promise<PlayLevel[]> {
    return this.dispatchRequest<PlayLevel[]>(
      `/ngb/api/v1/${ngbid}/PlayLevels/${tier2LeagueId}?season=${seasonId}`
    );
  }
  public getPlayLevelsByPlayType(
    ngbid: any,
    tier2LeagueId: string,
    playTypeId: string,
    seasonId: string
  ): Promise<PlayLevel[]> {
    return this.dispatchRequest<PlayLevel[]>(
      `/ngb/api/v1/${ngbid}/PlayLevels/ByType/${tier2LeagueId}?season=${seasonId}&playTypeId=${playTypeId}`
    );
  }
  public getPlayTypes(
    ngbid: any,
    tier2LeagueId: string,
    seasonId: string
  ): Promise<PlayType[]> {
    return this.dispatchRequest<PlayType[]>(
      `/ngb/api/v1/${ngbid}/PlayTypes/${tier2LeagueId}?season=${seasonId}`
    );
  }

  private dispatchRequest<T>(path): Promise<T> {
    this.isBusy = true;
    return axios({
      url: path,
      method: 'GET'
    })
      .then(resp => {
        return resp.data.data;
      })
      .finally(() => (this.isBusy = false));
  }
}
