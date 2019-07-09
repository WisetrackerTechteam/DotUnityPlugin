//
//  Environment.h
//  DOT
//
//  Created by Woncheol Heo on 2018. 6. 25..
//  Copyright © 2018년 wisetracker. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "DOTReachability.h"
#import <CoreTelephony/CTTelephonyNetworkInfo.h>
#import <CoreTelephony/CTCarrier.h>
@interface Environment : NSObject

- (NSString *)getLng;
- (NSString *)getCntr;
- (NSString *)getTz;
- (NSString *)getOs;
- (NSString *)getOsvr;
- (NSString *)getSr;
- (NSString *)getPhone;
- (NSString *)getApVr;
- (void)setGpsIsOn:(NSString *)gpsIsOn;
- (NSString *)getCari;
- (NSInteger)getIsWifi;
- (NSString *)getPlatfrom;
- (double)getInch;
@end

