
import Vue from 'vue';
import { Component, Emit, Prop, Watch } from 'vue-property-decorator';
import { ProgramService } from './program.service';

@Component({
  components: {}
})
export default class ProgramSetup extends Vue {
  public service: ProgramService = new ProgramService();

  public activityTypeId: number = null;
  public sportsAffiliationId: number = null;
  public organizationId: string = null;
  public tier1DistrictId: string = null;
  public tier2LeagueId: string = null;
  public tier3ClubId: string = null;
  public seasonId: string = null;
  public playLevelId: string = null;
  public playTypeId: string = null;
  public affiliations = [];
  public organizations = [];
  public tier1Districts = [];
  public tier2Leagues = [];
  public tier3Clubs = [];
  public seasons = [];
  public playLevels = [];
  public playTypes = [];
  @Prop() provider: string;
  @Prop() showTier1: boolean ;
  @Prop() showTier2: boolean ;
  @Prop() showTier3: boolean ;
  @Prop() showSeason: boolean ;
  @Prop() showPlayLevel: boolean ;
  @Prop() showPlayType: boolean ;
  @Prop() activityFilter: string = null;

  public get activityTypes() {
    if (this.activityFilter) {
      const tokens = this.activityFilter.split(',');
      return this.service.activityTypes.filter(x => {
        if (tokens.indexOf(x.name) >= 0) {
          return x;
        }
      });
    }
    return this.service.activityTypes;
  }
  public onActivityTypesChanged() {
    this.sportsAffiliationId = null;
    this.organizationId = null;
    this.tier1DistrictId = null;
    this.tier2LeagueId = null;
    this.tier3ClubId = null;
    this.seasonId = null;
    this.playLevelId = null;
    this.playTypeId = null;
    this.emitActivityTypeChanged('update', this.activityTypeId);
    if (this.activityTypeId === null) {
      return;
    }
    this.service.getSportsAffiliations().then(x => {
      let filtered = x.filter(y => y.activityTypeId === this.activityTypeId);
      if (this.provider) {
        filtered = filtered.filter(x => x.provider === this.provider);
      }
      this.affiliations = filtered;
    });
  }
  public onAffiliationsChanged() {
    this.organizationId = null;
    this.tier1DistrictId = null;
    this.tier2LeagueId = null;
    this.tier3ClubId = null;
    this.seasonId = null;
    this.playLevelId = null;
    this.playTypeId = null;
    this.emitAffiliationChanged('update', this.sportsAffiliationId);
    if (this.sportsAffiliationId === null) {
      return;
    }
    this.service
      .getOrganizations(this.sportsAffiliationId, this.activityTypeId)
      .then(x => {
        this.organizations = x;
      });
  }
  public onOrganizationsChanged() {
    this.tier1DistrictId = null;
    this.tier2LeagueId = null;
    this.tier3ClubId = null;
    this.seasonId = null;
    this.playLevelId = null;
    this.playTypeId = null;
    this.emitOrganizationChanged('update', this.organizationId);
    if (this.organizationId === null) {
      return;
    }
    if (this.showTier1) {
    } else {
      this.service
        .getTier2Leagues(this.sportsAffiliationId, this.organizationId)
        .then(x => (this.tier2Leagues = x));
    }
  }
  public onTier1Changed() {
    this.tier2LeagueId = null;
    this.tier3ClubId = null;
    this.seasonId = null;
    this.playLevelId = null;
    this.playTypeId = null;
    this.emitTier1Changed('update', this.tier1DistrictId);
  }
  public onTier2Changed() {
    this.tier3ClubId = null;
    this.seasonId = null;
    this.playLevelId = null;
    this.playTypeId = null;
    this.emitTier2Changed('update', this.tier2LeagueId);
    if (this.tier2LeagueId && this.showSeason) {
      this.service
        .getSeasons(this.sportsAffiliationId, this.tier2LeagueId)
        .then(x => (this.seasons = x));
    }
    if ( this.tier2LeagueId ) {
      console.log('getting clubs...');
      this.service.getTier3Clubs(this.sportsAffiliationId, this.tier2LeagueId).then( x =>  {
        console.log('Clubs', x);
        this.tier3Clubs = x;
      });
    }
   
    
  }
  public onTier3Changed() {
    this.emitTier3Changed('update', this.tier3ClubId);
  }
  public onSeasonChanged() {
    this.emitSeasonChanged('update', this.seasonId);
    if (this.tier2LeagueId && this.showPlayType) {
      this.service
        .getPlayTypes(this.sportsAffiliationId, this.tier2LeagueId, this.seasonId )
        .then(x => (this.playTypes = x));
    }
    if (this.tier2LeagueId && this.showPlayLevel) {
      this.service
        .getPlayLevels(this.sportsAffiliationId, this.tier2LeagueId, this.seasonId )
        .then(x => (this.playLevels = x));
    }
  }
  public onPlayTypeChanged() {
    this.emitPlayTypeChanged('update', this.playTypeId);
  }
  public onPlayLevelChanged() {
    this.emitPlayLevelChanged('update', this.playLevelId);
  }

  mounted() {
    // listen
    this.$eventHub.$on('FiltersReset' , (data ) => {
      this.tier1DistrictId = null;
      this.tier2LeagueId = null;
      this.tier3ClubId = null;
      this.seasonId  = null;
      this.playLevelId = null;
      this.playLevelId = null;
      this.activityTypeId = null;
      this.sportsAffiliationId = null;
      this.organizationId = null;
    }); 

  }
 @Emit('activityTypeChanged')
  emitActivityTypeChanged(mode: string, value: any) {}
 @Emit('affiliationChanged')
  emitAffiliationChanged(mode: string, value: any) {}
 @Emit('organizationChanged')
  emitOrganizationChanged(mode: string, value: string) {}
 @Emit('tier1Changed')
  emitTier1Changed(mode: string, value: string) {}
 @Emit('tier2Changed')
  emitTier2Changed(mode: string, value: string) {}
 @Emit('tier3Changed')
  emitTier3Changed(mode: string, value: string) {}
 @Emit('seasonChanged')
  emitSeasonChanged(mode: string, value: string) {}
 @Emit('playTypeChanged')
  emitPlayTypeChanged(mode: string, value: string) {}
 @Emit('playLevelChanged')
  emitPlayLevelChanged(mode: string, value: string) {}
}
